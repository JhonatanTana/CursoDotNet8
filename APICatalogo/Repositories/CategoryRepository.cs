using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;
public class CategoryRepository : ICategoriaRepository {

    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) {
        
        _context = context;
    }

    public IEnumerable<Categorias> GetCategorias() {

        return _context.Categorias.ToList();
    }

    public Categorias GetCategorias(int id) {

        return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
    }

    public Categorias Create(Categorias categoria) {

       if (categoria is null) {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Categorias Update(Categorias categoria) {

        if (categoria is null) {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return categoria;
    }

    public Categorias Delete(int id) {

        var categoria = _context.Categorias.Find(id);

        if(categoria is null) {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return categoria;
    }

}
