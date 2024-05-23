using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categorias>, ICategoriaRepository {
    public CategoriaRepository(AppDbContext context) : base(context) {
    }
}
