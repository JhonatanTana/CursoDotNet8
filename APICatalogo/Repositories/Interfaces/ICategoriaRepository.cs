using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository : IRepository<Categoria> {

    Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriasParams);
    Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParams);
}
