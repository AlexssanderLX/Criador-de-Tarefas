using GerenciadordeTarefasC_.Entities.Enums;
using GerenciadordeTarefasC_.Entities.Excessões;
using GerenciadordeTarefasC_.Services;

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
                return false;
                throw new ExcessõesPrograma("Erro tente novamente");
            }

            foreach (char c in titulo)
            {
                // Permite apenas letras e espaços
                if (!char.IsLetter(c))
                {
                    return false;
                    throw new ExcessõesPrograma("Erro tente novamente");
                }
                if (char.IsDigit(c))
                {
                    return false;
                    throw new ExcessõesPrograma("Erro tente novamente");
                }
            }
            return true;
        }





        public bool ValidarDataIncio(DateTime datainicio)
        {
            if (datainicio == DateTime.Now)
            {
                throw new ExcessõesPrograma("Deve ser uma data igual ou depois do que agora");
            }
            return true;
        }

        public bool ValidarPeriodoTarefa(DateTime DataFinal)
        {
            if(DataFinal < DateTime.Now) 
            {
                throw new ExcessõesPrograma("Deve ser uma data igual ou depois do que agora");
            }
            TimeSpan Diferença = DataFinal - DataInicio;
            return Diferença.TotalDays >= 1;
        }

        public static bool StatusFazSentidoParaPeriodo(DateTime dataInicio, DateTime dataVencimento, StatusdaTarefa status)
        {
            if (status == StatusdaTarefa.EmAndamento)
            {
                // Uma tarefa em andamento geralmente tem uma data de vencimento no futuro ou hoje.
                if (dataVencimento < DateTime.Now.Date)
                {
                    return false;
                }
            }

            // Validação específica para Concluida
            else if (status == StatusdaTarefa.Concluída)
            {
                // Uma tarefa concluída deve ter uma data de vencimento no passado ou hoje.
                if (dataVencimento > DateTime.Now.Date)
                {
                    return false;
                }
            }
            return true;
        }

        public Tarefas(string titulo, string descriçãoTarefa, DateTime datainicial, DateTime dataFinal, StatusdaTarefa status)
        {
            ValidarTitulo(titulo);
            ValidarPeriodoTarefa(dataFinal);
            ValidarDataIncio(datainicial);
            StatusFazSentidoParaPeriodo(datainicial, dataFinal, status);
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
