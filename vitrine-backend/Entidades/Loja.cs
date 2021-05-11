using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vitrine_backend.Entidades
{
    public class Loja : Entidade
    {
        public string Nome { get; set; }
        public string LogoUrl { get; set; }
        public Categoria Categoria { get; set; }
    }
}
