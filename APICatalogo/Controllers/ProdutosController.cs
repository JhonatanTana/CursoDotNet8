using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller {

        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork uof) {

            _uof = uof;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult <IEnumerable<Produtos>> GetProdutosCategroia(int id) {

            var produto = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if (produto is null) {
                
                return NotFound();
            }

            return Ok(produto);
        }


        [HttpGet]  // Retorna os produtos
        public ActionResult<IEnumerable<Produtos>> Get() {

            var produtos = _uof.ProdutoRepository.GetAll;

            if (produtos is null) {
                return NotFound("Produstos não encontrados ...");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")] // Retorna um produto especificado pelo ID
        public ActionResult<Produtos> Get(int id) { 

            var produtos = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produtos is null) {
                return NotFound("Produstos não encontrados ...");
            }

            return Ok(produtos);
        }

        [HttpPost]  // Adiciona um produto
        public  ActionResult Post(Produtos produto) {

            if (produto is null) {
                return BadRequest("Erro ao cadastrar o produto ...");
            }

            var novoProduto = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{Id:int}")] // Edita o produto por completo
        public ActionResult Put(int Id, Produtos produto) { 
            
            if (Id != produto.ProdutoId) {
                return BadRequest("Erro ao editar o produto ...");
            }

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")] // Deleta o produto pelo ID
        public ActionResult Delete(int id) {
            
            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if(produto is null) {
                return NotFound("Produto nao encontrado ...");
            }

            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            return Ok(produtoDeletado);
        }
    }
}
