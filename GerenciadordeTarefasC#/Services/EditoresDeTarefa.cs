using GerenciadordeTarefasC_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Interface para editores de Tarefas
namespace GerenciadordeTarefasC_.Services
{
    interface EditoresDeTarefa
    {
        public void Leitor(string dado);
    }
}
