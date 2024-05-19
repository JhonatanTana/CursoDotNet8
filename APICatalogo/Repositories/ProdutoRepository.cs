using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;
public class ProdutoRepository : IProdutoRepository {

    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context) {
        _context = context;
    }

    public IQueryable<Produtos> GetProdutos() {

        return _context.Produtos;
    }

    public Produtos GetProdutos(int id) {
        
        var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produtos is null) {

            throw new InvalidOperationException("Produto is null");
        }

        return produtos;
    }

    public Produtos Create(Produtos produtos) {

        if (produtos is null) {

            throw new InvalidOperationException("Produto is null");
        }
        
        _context.Produtos.Add(produtos);
        _context.SaveChanges();
        return produtos;
    }

    public bool Update(Produtos produtos) {

        if (produtos is null) {

            throw new InvalidOperationException("Produto is null");
        }

        if(_context.Produtos.Any(p => p.ProdutoId == produtos.ProdutoId)) { 
            
            _context.Produtos.Update(produtos);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Delete(int id) {
        
        var produto = _context.Produtos.Find(id);

        if (produto is not null) {

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
