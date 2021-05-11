using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vitrine_backend.Entidades
{
    public class Entidade
    {
        public Entidade()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime CriadoEm { get;set; }
    }
}
