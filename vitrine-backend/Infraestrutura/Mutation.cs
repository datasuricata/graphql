using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vitrine_backend.Entidades;

namespace vitrine_backend.Infraestrutura
{
    public class Mutation
    {
        public Categoria CriarCategoria([Service] AppDbContext context, string nomeCategoria, int prioridade)
        {
            var categoria = new Categoria
            {
                Nome = nomeCategoria,
                Prioridade = prioridade
            };

            context.Categoria.Add(categoria);
            context.SaveChanges();

            return categoria;
        }

        public Loja CriarLoja([Service] AppDbContext context, string nomeLoja, string logoUrl, string categoriaId)
        {
            var categoria = context.Categoria.FirstOrDefault(a => a.Id == Guid.Parse(categoriaId));

            var loja = new Loja
            {
                Nome = nomeLoja,
                LogoUrl = logoUrl,
                Categoria = categoria
            };

            context.Loja.Add(loja);
            context.SaveChanges();

            return loja;
        }

        public Faq CriarFaq([Service] AppDbContext context, string pergunta, string resposta)
        {
            var faq = new Faq
            {
                Pergunta = pergunta,
                Resposta = resposta
            };

            context.Faq.Add(faq);
            context.SaveChanges();

            return faq;
        }

        public Oferta CriarOferta([Service] AppDbContext context, string nome, double cashback, string lojaId)
        {
            var loja = context.Loja.FirstOrDefault(a => a.Id == Guid.Parse(lojaId));

            var oferta = new Oferta
            {
                Nome = nome,
                Cashback = cashback,
                Loja = loja
            };

            context.Oferta.Add(oferta);
            context.SaveChanges();

            return oferta;
        }
    }
}
