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
            string titulo = "", descriçao = "", statusDigitado = "", caminhopasta = "";
            DateTime Datainicio, Datafinal = DateTime.MinValue;
            GerenciadorDeTarefas Coleçao = new GerenciadorDeTarefas();
            CriarTarefas criarTarefas = new CriarTarefas();

            Console.WriteLine("Seja Bem vindo ao nosso Gerenciador de tarefas");
            Console.WriteLine("É a sua primeira vez criando tarefas? (responda com um caracter Sim/s ou Não/n)");

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
            do
            {
                if (confirmar1 == 'S' || confirmar1 == 's')
                {
                    Bconfirmar1 = true;
                    Console.WriteLine("Entendido!!! Seja bem vindo no seu primeiro uso!");
                    Console.WriteLine("Quantas tarefas voçê deseja Guardar?");


                    int quantidade1 = int.Parse(Console.ReadLine());
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
                        //mostrar na tela a lista
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





                else if (confirmar1 == 'N' || confirmar1 == 'n')
                {


                }
            } while (!Bconfirmar1);


        }
    }
}
