using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;
public class ProdutoRepository : Repository<Produtos>, IProdutoRepository {

    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context): base (context) { }

    public IEnumerable<Produtos> GetProdutosPorCategoria(int id) {
        
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
