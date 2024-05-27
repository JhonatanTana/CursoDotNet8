using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository {
    public CategoriaRepository(AppDbContext context) : base(context) {
    }

    public PagedList<Categoria> GetProdutos(CategoriaParameters categoriaParams) {

        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();
        var categoriasOrdenadas = PagedList<Categoria>.ToPageList(categorias, categoriaParams.PageNumber, categoriaParams.PageSize);
        return categoriasOrdenadas;
    }
}
