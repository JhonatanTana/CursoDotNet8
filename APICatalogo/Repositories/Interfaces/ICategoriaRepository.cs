using APICatalogo.Models;

namespace APICatalogo.Repositories;
public interface ICategoriaRepository : IRepository<Categorias> {
    object Update(object categorias);
}
