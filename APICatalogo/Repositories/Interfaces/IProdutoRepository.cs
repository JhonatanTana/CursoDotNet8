using APICatalogo.Models;

namespace APICatalogo.Repositories; 
public interface IProdutoRepository : IRepository<Produtos> {

    IEnumerable<Produtos> GetProdutosPorCategoria(int id);
}
