using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadordeTarefasC_.Entities.Excessões
{
    class ExcessõesPrograma : ApplicationException
    {
        public ExcessõesPrograma(string message) : base(message)
        {
        }
    }
}
