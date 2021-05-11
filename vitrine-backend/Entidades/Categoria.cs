using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vitrine_backend.Entidades
{
    public class Categoria : Entidade
    {
        public string Nome { get; set; }
        public int Prioridade { get; set; }
    }
}
