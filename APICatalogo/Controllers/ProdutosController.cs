using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller {

        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]  // Retorna os produtos
        public ActionResult<IEnumerable<Produtos>> Get() {

            var produtos = _context.Produtos.Take(10).ToList(); // Limita a 10 registros

            if (produtos is null) {
                return NotFound("Produstos não encontrados ...");
            }

            return produtos;
        }

        [HttpGet("{id:int}", Name ="ObterProduto")] // Retorna um produto especificado pelo ID
        public async Task<ActionResult<Produtos>> Get(int id) { // Metodo Assincrono

            var produtos = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produtos is null) {
                return NotFound("Produstos não encontrados ...");
            }

            return produtos;
        }

        [HttpPost]  // Adiciona um produto
        public  ActionResult Post(Produtos produto) {

            if (produto is null) {
                return BadRequest("Erro ao cadastrar o produto ...");
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{Id:int}")] // Edita o produto por completo
        public ActionResult Put(int Id, Produtos produto) { 
            
            if (Id != produto.ProdutoId) {
                return BadRequest("Erro ao editar o produto ...");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }

        [HttpDelete("{id:int}")] // Deleta o produto pelo ID
        public ActionResult Delete(int id) {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            //var produto = _context.Produtos.Find(id);

            if (produto is null) {
                return NotFound("Produto não localizado...");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

    }
}
