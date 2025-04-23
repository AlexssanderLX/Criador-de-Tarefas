using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using GerenciadordeTarefasC_.Entities;
using GerenciadordeTarefasC_.Entities.Enums;
using GerenciadordeTarefasC_.Services;

namespace GerenciadordeTarefasC_.Services
{
    internal class ModificarTarefa : EditoresDeTarefa
    {
        public SortedDictionary<string, Tarefas> tarefasPorTitulo = new SortedDictionary<string, Tarefas>();
        private List<Tarefas> listaDeTarefas = new List<Tarefas>();
        private string nomeArquivo;

        public void Leitor(string dado)
        {
            nomeArquivo = Path.GetFileName(dado);
            try
            {
                using (StreamReader sr = File.OpenText(dado))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith("Título: "))
                        {
                            string titulo = linha.Substring("Título: ".Length);
                            string descrição = sr.ReadLine()?.Substring("Descrição: ".Length);
                            DateTime datainicio = DateTime.Parse(sr.ReadLine()?.Substring("Data de Inicio: ".Length));
                            string dataFinalStr = sr.ReadLine()?.Substring("Data Final: ".Length);
                            DateTime datafinal;
                            if (!DateTime.TryParse(dataFinalStr, out datafinal))
                            {
                                Console.WriteLine($"Aviso: Formato de data final inválido para a tarefa '{titulo}'. Ignorando.");
                                for (int i = 0; i < 2; i++) sr.ReadLine();
                                continue;
                            }
                            StatusdaTarefa statuslido = (StatusdaTarefa)Enum.Parse(typeof(StatusdaTarefa), sr.ReadLine()?.Substring("Status: ".Length));
                            StatusdaTarefa statusFinal = statuslido;

                            if (datafinal < DateTime.Now.Date)
                            {
                                Console.WriteLine($"Aviso: A data final da tarefa '{titulo}' expirou em '{datafinal}'. O status será atualizado para Concluída.");
                                statusFinal = StatusdaTarefa.Concluída;
                            }

                            Tarefas task = new Tarefas(titulo, descrição, datainicio, datafinal, statusFinal, true);
                            Console.WriteLine($"Tarefa lida: Título='{task.Titulo}', Status='{task.Status}'"); // ADICIONE ESTA LINHA
                            if (!tarefasPorTitulo.ContainsKey(titulo))
                            {
                                tarefasPorTitulo.Add(titulo, task);
                                Console.WriteLine($"Tarefa adicionada ao dicionário: '{titulo}'"); // ADICIONE ESTA LINHA
                            }
                            listaDeTarefas.Add(task);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Erro: O arquivo '{dado}' não foi encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao ler o arquivo: {ex.Message}");
            }
        }

        public void AdicionarTarefa(Tarefas novaTarefa)
        {
            if (!tarefasPorTitulo.ContainsKey(novaTarefa.Titulo))
            {
                tarefasPorTitulo.Add(novaTarefa.Titulo, novaTarefa);
                listaDeTarefas.Add(novaTarefa);
                Console.WriteLine($"Tarefa '{novaTarefa.Titulo}' adicionada com sucesso.");
            }
            else
            {
                Console.WriteLine($"Erro: Já existe uma tarefa com o título '{novaTarefa.Titulo}'.");
            }
        }

        public void RemoverTarefa(string tituloRemover)
        {
            // Busca a tarefa diretamente pela chave (título) fornecida
            if (tarefasPorTitulo.ContainsKey(tituloRemover))
            {
                Tarefas tarefaParaRemover = tarefasPorTitulo[tituloRemover];
                tarefasPorTitulo.Remove(tituloRemover);
                listaDeTarefas.RemoveAll(t => t.Titulo == tituloRemover);
                Console.WriteLine($"Tarefa com o título '{tituloRemover}' removida com sucesso.");
                SalvarListaDeTarefas(nomeArquivo, listaDeTarefas);
            }
            else
            {
                Console.WriteLine($"Erro: Tarefa com o título '{tituloRemover}' não encontrada.");
            }
        }


        public void SalvarTarefas(string caminhoArquivo)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(caminhoArquivo))
                {
                    foreach (var tarefaPar in tarefasPorTitulo)
                    {
                        sw.WriteLine(tarefaPar.Value.ToString()); // Usando a formatação da classe Tarefas
                    }
                }
                Console.WriteLine($"Tarefas salvas com sucesso em: {caminhoArquivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar tarefas: {ex.Message}");
            }
        }
        public SortedDictionary<string, Tarefas> OrdenarTarefas(CriterioOrdenacao criterio)
        {
            IOrderedEnumerable<KeyValuePair<string, Tarefas>> tarefasOrdenadas;

            switch (criterio)
            {
                case CriterioOrdenacao.Titulo:
                    tarefasOrdenadas = tarefasPorTitulo.OrderBy(kvp => kvp.Value.Titulo);
                    break;
                case CriterioOrdenacao.DataInicio:
                    tarefasOrdenadas = tarefasPorTitulo.OrderBy(kvp => kvp.Value.DataInicio);
                    break;
                case CriterioOrdenacao.DataFinal:
                    tarefasOrdenadas = tarefasPorTitulo.OrderBy(kvp => kvp.Value.DataFinal);
                    break;
                case CriterioOrdenacao.Status:
                    tarefasOrdenadas = tarefasPorTitulo.OrderBy(kvp => kvp.Value.Status);
                    break;
                default:
                    Console.WriteLine("Critério de ordenação inválido. Retornando lista original.");
                    return tarefasPorTitulo;
            }

            // Converter para um Dictionary e depois para SortedDictionary para manter a ordem
            var tarefasDictionary = tarefasOrdenadas.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return new SortedDictionary<string, Tarefas>(tarefasDictionary);
        }

        public void SalvarTarefasOrdenadasPorEscolha(string caminhoArquivo, CriterioOrdenacao criterio)
        {
            SortedDictionary<string, Tarefas> tarefasOrdenadas = OrdenarTarefas(criterio);
            SalvarListaDeTarefas(caminhoArquivo, tarefasOrdenadas.Values);
        }

        private void SalvarListaDeTarefas(string caminhoArquivo, IEnumerable<Tarefas> listaDeTarefas)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(caminhoArquivo))
                {
                    foreach (var tarefa in listaDeTarefas)
                    {
                        sw.WriteLine(tarefa.ToString()); // Usando a formatação da classe Tarefas
                    }
                }
                Console.WriteLine($"Lista de tarefas salva com sucesso em: {caminhoArquivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar a lista de tarefas: {ex.Message}");
            }
        }
    }
}

