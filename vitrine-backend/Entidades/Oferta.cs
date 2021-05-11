using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vitrine_backend.Entidades
{
    public class Oferta : Entidade
    {
        public string Nome { get; set; }
        public double Cashback { get; set; } 
        public Loja Loja { get; set; }
    }
}
