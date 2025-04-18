using GerenciadordeTarefasC_.Entities.Enums;
using GerenciadordeTarefasC_.Entities.Excessões;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GerenciadordeTarefasC_.Services
{
    public class CriarTarefas
    {
        public static void CriarArquivoDeTarefas(string caminhoPasta, string nomeArquivo, SortedDictionary<string, Entities.Tarefas> tarefas)
        {
            string caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo + ".txt");

            try
            {
                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                    Console.WriteLine($"Diretório criado em: {caminhoPasta}");
                }

                // Escreve as tarefas no arquivo de texto usando a formatação do ToString()
                using (StreamWriter writer = new StreamWriter(caminhoCompleto))
                {
                    foreach (var par in tarefas)
                    {
                        writer.WriteLine(par.Value.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar/escrever no arquivo: {ex.Message}");
            }
        }
    }
}