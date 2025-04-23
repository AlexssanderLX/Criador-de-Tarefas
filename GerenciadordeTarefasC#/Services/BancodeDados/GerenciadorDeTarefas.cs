using GerenciadordeTarefasC_.Entities.Enums;
using GerenciadordeTarefasC_.Entities.Excessões;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GerenciadordeTarefasC_.Entities
{
    public class GerenciadorDeTarefas : IEnumerable<Tarefas>
    {
        public SortedDictionary<string, Tarefas> tarefasPorTitulo = new SortedDictionary<string, Tarefas>();

        // Construtor padrão
        public GerenciadorDeTarefas()
        {
            tarefasPorTitulo = new SortedDictionary<string, Tarefas>();
            
        }

        public void AdicionarTarefa(Tarefas tarefa)
        {
            if (tarefasPorTitulo.ContainsKey(tarefa.Titulo))
            {
                throw new ExcessõesPrograma($"Já existe uma tarefa com o título '{tarefa.Titulo}'.");
            }
            tarefasPorTitulo.Add(tarefa.Titulo, tarefa);
            
        }
        public SortedDictionary<string, Tarefas> TarefasPorTitulo
        {
            get { return tarefasPorTitulo; }
        }

        public IEnumerator<Tarefas> GetEnumerator()
        {
            return tarefasPorTitulo.Values.GetEnumerator();
        }

        // Implementação explícita da interface IEnumerable (não genérica)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}