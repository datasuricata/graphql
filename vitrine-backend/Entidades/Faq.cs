using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vitrine_backend.Entidades
{
    public class Faq : Entidade
    {
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
    }
}
