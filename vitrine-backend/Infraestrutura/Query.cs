using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vitrine_backend.Entidades;

namespace vitrine_backend.Infraestrutura
{
    //representa as consultas no banco de dados
    public class Query
    {
        //representa uma consulta para trazer informações da categoria
        [UseFiltering]
        public IQueryable<Categoria> GetCategoria([Service] AppDbContext context)
        {
            return context.Categoria;
        }

        [UseFiltering]
        public IQueryable<Loja> GetLoja([Service] AppDbContext context)
        {
            return context.Loja.Include(i => i.Categoria);
        }

        [UseFiltering]
        public IQueryable<Oferta> GetOferta([Service] AppDbContext context)
        {
            return context.Oferta.Include(i => i.Loja).Include(i => i.Loja.Categoria);
        }

        [UseFiltering]
        public IQueryable<Faq> GetFaq([Service] AppDbContext context)
        {
            return context.Faq;
        }
    }
}
