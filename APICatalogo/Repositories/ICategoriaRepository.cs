using APICatalogo.Models;

namespace APICatalogo.Repositories; 
public interface ICategoriaRepository {

    IEnumerable<Categorias> GetCategorias();
    Categorias GetCategorias(int id);
    Categorias Create(Categorias categoria);
    Categorias Update(Categorias categoria);
    Categorias Delete(int id);
}
