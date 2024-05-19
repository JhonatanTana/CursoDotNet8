using APICatalogo.Models;

namespace APICatalogo.Repositories; 
public interface IProdutoRepository {

    IQueryable<Produtos> GetProdutos();
    Produtos GetProdutos(int id);
    Produtos Create(Produtos produtos);
    bool Update(Produtos produtos);
    bool Delete(int id);
}
