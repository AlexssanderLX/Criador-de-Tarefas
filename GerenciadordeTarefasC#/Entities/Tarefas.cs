using GerenciadordeTarefasC_.Entities.Enums;
using GerenciadordeTarefasC_.Entities.Excessões;

namespace GerenciadordeTarefasC_.Entities
{
    public class Tarefas
    {
        public string Titulo { get; set; }
        public string DescriçãoTarefa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public StatusdaTarefa Status { get; set; }

        public Tarefas()
        { }

        public bool ValidarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo) || titulo.Length > 15)
            {
                throw new ExcessõesPrograma("Erro: O título deve ter entre 1 e 15 caracteres.");
            }

            foreach (char c in titulo)
            {
                if (!char.IsLetter(c) && c != ' ') // Permite letras e espaços
                {
                    throw new ExcessõesPrograma("Erro: O título deve conter apenas letras e espaços.");
                }
                if (char.IsDigit(c))
                {
                    throw new ExcessõesPrograma("Erro: O título não deve conter números.");
                }
            }
            return true;
        }

        public bool ValidarDataIncio(DateTime datainicio)
        {
            if (datainicio < DateTime.Now.Date) // Permitir data igual ao Now (ajustado para Date)
            {
                throw new ExcessõesPrograma("Erro: A data de início deve ser igual ou depois de hoje.");
            }
            return true;
        }

        public bool ValidarPeriodoTarefa(DateTime DataFinal)
        {
            if (DataFinal < DateTime.Now.Date) // Ajustado para Date
            {
                throw new ExcessõesPrograma("Erro: A data final deve ser igual ou depois de hoje.");
            }
            TimeSpan Diferença = DataFinal - DataInicio;
            if (Diferença.TotalDays < 1)
            {
                throw new ExcessõesPrograma("Erro: O período da tarefa deve ser de pelo menos 1 dia.");
            }
            return true;
        }

        public static bool StatusFazSentidoParaPeriodo(DateTime dataInicio, DateTime dataVencimento, StatusdaTarefa status)
        {
            if (status == StatusdaTarefa.EmAndamento)
            {
                if (dataVencimento < DateTime.Now.Date)
                {
                    return false; // Retorna false, a lógica de correção estará fora da validação
                }
            }
            else if (status == StatusdaTarefa.Concluída)
            {
                if (dataVencimento > DateTime.Now.Date)
                {
                    return false; // Retorna false, a lógica de correção estará fora da validação
                }
            }
            return true;
        }
        public Tarefas(string titulo, string descriçãoTarefa, DateTime dataInicio, DateTime dataFinal, StatusdaTarefa status, bool fromFile)
        {
            Titulo = titulo;
            DescriçãoTarefa = descriçãoTarefa;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            Status = status;
            // As validações são ignoradas neste construtor
        }
        public Tarefas(string titulo, string descriçãoTarefa, DateTime datainicial, DateTime dataFinal, StatusdaTarefa status)
        {
            ValidarTitulo(titulo);
            ValidarPeriodoTarefa(dataFinal);
            ValidarDataIncio(datainicial);
            if (!StatusFazSentidoParaPeriodo(datainicial, dataFinal, status))
            {
                throw new ExcessõesPrograma("Erro: O status da tarefa não faz sentido para o período.");
            }
            Titulo = titulo;
            DescriçãoTarefa = descriçãoTarefa;
            DataInicio = datainicial;
            DataFinal = dataFinal;
            Status = status;
        }
        public override string ToString()
        {
            return $"Título: {Titulo}" + Environment.NewLine +
                   $"Descrição: {DescriçãoTarefa}" + Environment.NewLine +
                   $"Data de Inicio: {DataInicio.ToShortDateString()}" + Environment.NewLine +
                   $"Data Final: {DataFinal.ToShortDateString()}" + Environment.NewLine +
                   $"Status: {Status}" + Environment.NewLine +
                   "<<<<<<<FIM DA TAREFA>>>>>>>" + Environment.NewLine;
        }
    }
}