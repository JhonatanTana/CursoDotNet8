using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;
public class ProdutoRepository : Repository<Produto>, IProdutoRepository {

    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context): base (context) { }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtoParams) {

        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPageList(produtos, produtoParams.PageNumber, produtoParams.PageSize);
        return produtosOrdenados;
    }

    /*public IEnumerable<Produto> GetProdutos(ProdutosParameters produtoParams) {

        return GetAll().OrderBy(p => p.Nome)
            .Skip((produtoParams.PageNumber - 1)* produtoParams.PageSize)
            .Take(produtoParams.PageSize)
            .ToList();
    }*/

    public IEnumerable<Produto> GetProdutosPorCategoria(int id) {
        
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
