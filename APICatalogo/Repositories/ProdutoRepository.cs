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

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams) {

        var produtos = GetAll().AsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio)) {

            if(produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase)) {

                produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if(produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase)) {

                produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase)) {

                produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
        }

        var produtosFiltrados = PagedList<Produto>.ToPageList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);
        return produtosFiltrados;
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
