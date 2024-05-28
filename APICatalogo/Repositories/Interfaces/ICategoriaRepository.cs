using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository : IRepository<Categoria> {

    PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNome);
    PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParams);

}
