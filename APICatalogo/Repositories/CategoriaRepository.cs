using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository {
    public CategoriaRepository(AppDbContext context) : base(context) {
    }

    public PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParams) {

        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();
        var categoriasOrdenadas = PagedList<Categoria>.ToPageList(categorias, categoriaParams.PageNumber, categoriaParams.PageSize);
        return categoriasOrdenadas;
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams) {

        var categorias = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriasParams.Nome)) {

            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome));
        }

        var categoriasFiltradads = PagedList<Categoria>.ToPageList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
        return categoriasFiltradads;
    }
}
