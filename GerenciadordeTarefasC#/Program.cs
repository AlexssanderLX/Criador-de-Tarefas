using System;
using System.Collections.Generic;
using System.Linq;
using GerenciadordeTarefasC_.Entities;
using GerenciadordeTarefasC_.Entities.Enums;
using GerenciadordeTarefasC_.Entities.Excessões;
using GerenciadordeTarefasC_.Services;

namespace Gerenciador_de_Tarefas
{
    class Program
    {

        static void Main(string[] args)
        {
            string titulo = "", descriçao = "", statusDigitado = "", caminhopasta = "", caminhopasta2 = "", escolhaOrdenacao = "";
            DateTime Datainicio, Datafinal = DateTime.MinValue;
            ModificarTarefa modificarTarefa = new ModificarTarefa();
            GerenciadorDeTarefas Coleçao = new GerenciadorDeTarefas();
            GerenciadorDeTarefas Coleçao2 = new GerenciadorDeTarefas();
            CriarTarefas criarTarefas = new CriarTarefas();

            Console.WriteLine("Seja Bem vindo ao nosso Gerenciador de tarefas");
            Console.WriteLine("É a sua primeira vez criando tarefas? (digite corretamente s ou n usando um caracter)");

            char confirmar1 = 'A';
            bool digiteSouN = false;
            do
            {
                try
                {
                    string input = Console.ReadLine();
                    if (input.Length == 1) // Garante que apenas um caractere foi digitado
                    {
                        confirmar1 = char.Parse(input);
                        if (confirmar1 == 'S' || confirmar1 == 's' || confirmar1 == 'N' || confirmar1 == 'n')
                        {
                            digiteSouN = true;
                        }
                        else
                        {
                            Console.WriteLine("Digite corretamente s ou n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Digite corretamente s ou n");
                    Console.WriteLine("<__________>");
                    Console.WriteLine();
                }
            } while (!digiteSouN);


            bool Bconfirmar1 = false;


            if (confirmar1 == 'S' || confirmar1 == 's')
            {
                Bconfirmar1 = true;
                Console.WriteLine("Entendido!!! Seja bem vindo no seu primeiro uso!");
                Console.WriteLine("Quantas tarefas voçê deseja Guardar?");

                //verificação quantidade
                bool Bquantidade1 = false;
                int quantidade1 = 0;
                do
                {
                    quantidade1 = int.Parse(Console.ReadLine());
                    if (quantidade1 == 0)
                    {
                        Console.WriteLine("Retorne um valor maior que zero");
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                    else if (quantidade1 > 0)
                    {
                        Console.WriteLine("Certo seguiremos!");
                        Bquantidade1 = true;
                    }
                } while (!Bquantidade1);

                if (quantidade1 > 0)
                {
                    for (int i = 0; i < quantidade1; i++)
                    {
                        try
                        {
                            Console.WriteLine("Digite um titulo: (máximo 25 caracteres)");

                            //verificação de entrada do titulo
                            bool Btitulo = false;
                            Tarefas A = new Tarefas();
                            do
                            {
                                titulo = Console.ReadLine();
                                if (A.ValidarTitulo(titulo))
                                {
                                    Btitulo = true;
                                }

                                else
                                {
                                    Console.WriteLine("Digite corretamente");
                                    Console.WriteLine("<__________>");
                                    Console.WriteLine();
                                }
                            } while (!Btitulo);


                            Console.WriteLine("Digite uma descrição: ");
                            descriçao = Console.ReadLine();
                            Datainicio = DateTime.Now;

                            //verificação de entrada da data final
                            Console.WriteLine("Digite uma data para conclusão da tarefa: (deve ser depois de agorqa)");
                            A = new Tarefas();
                            bool BDatafinal = false;
                            do
                            {
                                try
                                {
                                    Datafinal = DateTime.Parse(Console.ReadLine());
                                    if (A.ValidarPeriodoTarefa(Datafinal))
                                    {
                                        BDatafinal = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Digite corretamente a data sendo ela posterior a agora!!!");
                                        Console.WriteLine("<__________>");
                                        Console.WriteLine();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("<__________>");
                                    Console.WriteLine();
                                }
                            } while (!BDatafinal);

                            Console.WriteLine("digite EmAndamento (exatamente dessa forma)");
                            statusDigitado = Console.ReadLine();
                            while (statusDigitado != "EmAndamento")
                            {
                                Console.WriteLine("Deve ser digitado exatamente da forma que foi proposta");
                                statusDigitado = Console.ReadLine();
                            }
                            StatusdaTarefa status = (StatusdaTarefa)Enum.Parse(typeof(StatusdaTarefa), statusDigitado, true);
                            Tarefas tarefas = new Tarefas(titulo, descriçao, Datainicio, Datafinal, status);
                            Coleçao.AdicionarTarefa(tarefas);
                            Console.WriteLine();
                            Console.WriteLine("<__________>");
                            Console.WriteLine();

                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ocorreu um erro {ex.Message}");
                        }
                    }
                    //mostrar na tela a lista exemplo
                    /*Console.WriteLine("Suas tarefas:");
                    foreach (var tarefaNaColecao in Coleçao)
                    {
                        Console.WriteLine(tarefaNaColecao);
                    }
                    Console.WriteLine();*/
                }
                Console.WriteLine("Agora que você criou suas tarefas informe no campo abaixo o nome do arquivo que voçê quer adicionar suas tarefas:  ");

                bool Bcaminhopasta = false;
                do
                {
                    caminhopasta = Console.ReadLine();
                    if (!Directory.Exists(caminhopasta))
                    {
                        Console.WriteLine("Digite corretamente o local do diretório");
                        Bcaminhopasta = false;
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                    else
                    {
                        Bcaminhopasta = true;
                        Console.WriteLine("Aquivo Encontrado");
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                } while (!Bcaminhopasta);
                Console.WriteLine("Agora digite o nome do aruivo txt com suas tarefas que voçê quer criar: ");

                string nomearquivo;
                bool nomeArquivoValido = false;

                do
                {
                    Console.WriteLine("Digite o nome do arquivo (sem extensão):");
                    nomearquivo = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nomearquivo))
                    {
                        Console.WriteLine("O nome do arquivo não pode estar vazio. Digite novamente.");
                    }
                    else if (nomearquivo.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                    {
                        Console.WriteLine("O nome do arquivo contém caracteres inválidos. Digite novamente.");
                        Console.WriteLine($"Caracteres inválidos: {string.Join(", ", Path.GetInvalidFileNameChars())}");
                    }
                    // Opcional: Adicionar outras verificações, como comprimento máximo
                    else if (nomearquivo.Length > 255) // Exemplo de limite de comprimento
                    {
                        Console.WriteLine("O nome do arquivo é muito longo. Digite novamente (máximo 255 caracteres).");
                    }
                    else
                    {
                        nomeArquivoValido = true;
                    }

                } while (!nomeArquivoValido);

                Console.WriteLine($"Nome do arquivo digitado: {nomearquivo}");

                CriarTarefas.CriarArquivoDeTarefas(caminhopasta, nomearquivo, Coleçao.TarefasPorTitulo);
            }











            //Lógica para ler determinado arquivo e realizar as modificações

            else if (confirmar1 == 'N' || confirmar1 == 'n')
            {
                Console.WriteLine("Certo muito Bom!!!");
                Console.WriteLine("Digite então o nome do diretório onde voçê deseja realizar alterações: ");

                bool Bcaminhopasta2 = false;
                do
                {
                    caminhopasta2 = Console.ReadLine();
                    if (!File.Exists(caminhopasta2))
                    {
                        Console.WriteLine("Digite corretamente o local do diretório");
                        Bcaminhopasta2 = false;
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                    else
                    {
                        Bcaminhopasta2 = true;
                        Console.WriteLine("Aquivo Encontrado");
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                } while (!Bcaminhopasta2);

                 modificarTarefa = new ModificarTarefa();
                modificarTarefa.Leitor(caminhopasta2);

                Console.WriteLine("Deseja adicionar tarefas neste diretório? (digite corretamente s ou n usando um caracter)");
                char confirmar2 = 'A';
                bool digiteSouN2 = false;
                do
                {
                    try
                    {
                        string input = Console.ReadLine();
                        if (input.Length == 1) // Garante que apenas um caractere foi digitado
                        {
                            confirmar2 = char.Parse(input);
                            if (confirmar2 == 'S' || confirmar2 == 's' || confirmar2 == 'N' || confirmar2 == 'n')
                            {
                                digiteSouN2 = true;
                            }
                            else
                            {
                                Console.WriteLine("Digite corretamente s ou n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Digite corretamente s ou n");
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                } while (!digiteSouN2);

                // lógica para caso seja sim o desejo de adicionar tarefas
                if (confirmar2 == 's' || confirmar2 == 'S')
                {
                    Console.WriteLine("Deseja adicionar quantas tarefas?");
                    //verificação quantidade
                    bool Bquantidade1 = false;
                    int quantidade1 = 0;
                    do
                    {
                        quantidade1 = int.Parse(Console.ReadLine());
                        if (quantidade1 == 0)
                        {
                            Console.WriteLine("Retorne um valor maior que zero");
                            Console.WriteLine("<__________>");
                            Console.WriteLine();
                        }
                        else if (quantidade1 > 0)
                        {
                            Console.WriteLine("Certo seguiremos!");
                            Bquantidade1 = true;
                        }
                    } while (!Bquantidade1);


                    for (int i = 0; i < quantidade1; i++)
                    {
                        try
                        {
                            Console.WriteLine("Digite um titulo: (máximo 25 caracteres)");

                            //verificação de entrada do titulo
                            bool Btitulo = false;
                            Tarefas A = new Tarefas();
                            do
                            {
                                titulo = Console.ReadLine();
                                if (A.ValidarTitulo(titulo))
                                {
                                    Btitulo = true;
                                }

                                else
                                {
                                    Console.WriteLine("Digite corretamente");
                                    Console.WriteLine("<__________>");
                                    Console.WriteLine();
                                }
                            } while (!Btitulo);


                            Console.WriteLine("Digite uma descrição: ");
                            descriçao = Console.ReadLine();
                            Datainicio = DateTime.Now;

                            //verificação de entrada da data final
                            Console.WriteLine("Digite uma data para conclusão da tarefa: (deve ser depois de agorqa)");
                            A = new Tarefas();
                            bool BDatafinal = false;
                            do
                            {
                                try
                                {
                                    Datafinal = DateTime.Parse(Console.ReadLine());
                                    if (A.ValidarPeriodoTarefa(Datafinal))
                                    {
                                        BDatafinal = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Digite corretamente a data sendo ela posterior a agora!!!");
                                        Console.WriteLine("<__________>");
                                        Console.WriteLine();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("<__________>");
                                    Console.WriteLine();
                                }
                            } while (!BDatafinal);

                            Console.WriteLine("digite EmAndamento (exatamente dessa forma)");
                            statusDigitado = Console.ReadLine();
                            while (statusDigitado != "EmAndamento")
                            {
                                Console.WriteLine("Deve ser digitado exatamente da forma que foi proposta");
                                statusDigitado = Console.ReadLine();
                            }
                            StatusdaTarefa status = (StatusdaTarefa)Enum.Parse(typeof(StatusdaTarefa), statusDigitado, true);
                            Tarefas tarefas2 = new Tarefas(titulo, descriçao, Datainicio, Datafinal, status);
                            modificarTarefa.AdicionarTarefa(tarefas2);
                            Console.WriteLine();
                            Console.WriteLine("<__________>");
                            Console.WriteLine();

                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ocorreu um erro {ex.Message}");
                        }
                    }

                }

                Console.WriteLine("Deseja remover alguma tarefa?");
                char confirmar3 = 'A';
                bool digiteSouN3 = false;
                do
                {
                    try
                    {
                        string input3 = Console.ReadLine();
                        if (input3.Length == 1) // Garante que apenas um caractere foi digitado
                        {
                            confirmar3 = char.Parse(input3);
                            if (confirmar3 == 'S' || confirmar3 == 's' || confirmar3 == 'N' || confirmar3 == 'n')
                            {
                                digiteSouN3 = true;
                            }
                            else
                            {
                                Console.WriteLine("Digite corretamente s ou n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Digite corretamente s ou n");
                        Console.WriteLine("<__________>");
                        Console.WriteLine();
                    }
                } while (!digiteSouN3);

                Console.WriteLine("Quantas tarefas deseja remover?");
                int quantidadeRemover = 0;
                bool quantidadeValida = false;

                do
                {
                    try
                    {
                        string inputQuantidade = Console.ReadLine();
                        if (int.TryParse(inputQuantidade, out quantidadeRemover) && quantidadeRemover >= 0)
                        {
                            quantidadeValida = true;
                        }
                        else
                        {
                            Console.WriteLine("Digite uma quantidade válida (número inteiro não negativo).");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                        Console.WriteLine("Digite uma quantidade válida (número inteiro não negativo).");
                    }
                } while (!quantidadeValida);

                Console.WriteLine("Chaves no dicionário antes da busca:");

                if (quantidadeRemover > 0)
                {
                    for (int i = 0; i < quantidadeRemover; i++)
                    {
                        bool tarefaRemovida = false;
                        do
                        {
                            // *** BLOCO DE LOG PARA IMPRIMIR AS CHAVES DO DICIONÁRIO ***
                            Console.WriteLine("Chaves no dicionário antes da busca:");
                            foreach (var chave in modificarTarefa.tarefasPorTitulo.Keys)
                            {
                                Console.WriteLine($"- '{chave}'");
                            }
                            // *** FIM DO BLOCO DE LOG ***

                            Console.WriteLine($"Digite o título da tarefa que você deseja remover ({i + 1}/{quantidadeRemover}):");
                            string tituloRemover = Console.ReadLine();

                            modificarTarefa.RemoverTarefa(tituloRemover);

                            if (!modificarTarefa.tarefasPorTitulo.ContainsKey(tituloRemover))
                            {
                                tarefaRemovida = true;
                            }
                            else
                            {
                                Console.WriteLine($"Tarefa com o título '{tituloRemover}' não encontrada. Digite novamente.");
                            }
                        } while (!tarefaRemovida);
                    }
                    Console.WriteLine($"As {quantidadeRemover} tarefas foram processadas para remoção.");
                }
                else
                {
                    Console.WriteLine("Nenhuma tarefa será removida.");
                }

                Console.WriteLine("<__________>");
                Console.WriteLine();
            }



            // lógica de organizar

            Console.WriteLine("Ok continuaremos!!!");
            Console.WriteLine("Agora com o arquivo todas as tarefas que o usuário deseja atualizado");
            Console.WriteLine("Pretende organizar o arquivo de alguma maneira?");
            // confirmar se existe o desejo de ordenar
            char confirmar4 = 'A';
            bool digiteSouN4 = false;
            do
            {
                try
                {
                    string input4 = Console.ReadLine();
                    if (input4.Length == 1) // Garante que apenas um caractere foi digitado
                    {
                        confirmar4 = char.Parse(input4);
                        if (confirmar4 == 'S' || confirmar4 == 's' || confirmar4 == 'N' || confirmar4 == 'n')
                        {
                            digiteSouN4 = true;
                        }
                        else
                        {
                            Console.WriteLine("Digite corretamente s ou n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Digite corretamente s ou n");
                    Console.WriteLine("<__________>");
                    Console.WriteLine();
                }
            } while (!digiteSouN4);

            if (confirmar4 == 'S' || confirmar4 == 's')
            {
                Console.WriteLine("Digite então como voçê deseja ordenar");
                Console.WriteLine("Digite exatamente da forma pelo qual será proposta: ");

                Console.WriteLine("Ordenar por: (Titulo, DataInicio, DataFinal, Status)");
                bool BescolhaOrdenacao = false;
                do
                {
                    try
                    {
                        escolhaOrdenacao = Console.ReadLine();
                        if (escolhaOrdenacao == "Titulo" || escolhaOrdenacao == "DataInicio" || escolhaOrdenacao == "DataFinal" || escolhaOrdenacao == "Status")
                        {
                            Console.WriteLine("Status correto!!!");
                            BescolhaOrdenacao = true;
                        }
                        else
                        {
                            Console.WriteLine("Digite corretamente conforme o fornecido");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (!BescolhaOrdenacao);

                if (Enum.TryParse<CriterioOrdenacao>(escolhaOrdenacao, true, out CriterioOrdenacao criterioOrdenacao))
                {
                    // A conversão foi bem-sucedida, você pode usar 'criterioOrdenacao'
                    modificarTarefa.SalvarTarefasOrdenadasPorEscolha(caminhopasta2, criterioOrdenacao);
                }
                else
                {
                    // A conversão falhou (o usuário digitou algo inválido)
                    Console.WriteLine("Opção de ordenação inválida. Por favor, digite uma das opções listadas.");
                }
                Console.WriteLine("Está tudo salvo no seu arquivo obrigado");
            }
        }
    }
}

