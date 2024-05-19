using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller {

        private readonly IProdutoRepository _produtoRepository;
        private readonly IRepository<Produtos> _repository;

        public ProdutosController(IProdutoRepository produtoRepository, IRepository<Produtos> repository) {

            _produtoRepository = produtoRepository;
            _repository = repository;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult <IEnumerable<Produtos>> GetProdutosCategroia(int id) {

            var produto = _produtoRepository.GetProdutosPorCategoria(id);

            if (produto is null) {
                
                return NotFound();
            }

            return Ok(produto);
        }


        [HttpGet]  // Retorna os produtos
        public ActionResult<IEnumerable<Produtos>> Get() {

            var produtos = _repository.GetAll;

            if (produtos is null) {
                return NotFound("Produstos não encontrados ...");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")] // Retorna um produto especificado pelo ID
        public ActionResult<Produtos> Get(int id) { 

            var produtos = _repository.Get(p => p.ProdutoId == id);

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

            var novoProduto = _repository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{Id:int}")] // Edita o produto por completo
        public ActionResult Put(int Id, Produtos produto) { 
            
            if (Id != produto.ProdutoId) {
                return BadRequest("Erro ao editar o produto ...");
            }

            var produtoAtualizado = _repository.Update(produto);
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")] // Deleta o produto pelo ID
        public ActionResult Delete(int id) {
            
            var produto = _repository.Get(p => p.ProdutoId == id);

            if(produto is null) {
                return NotFound("Produto nao encontrado ...");
            }

            var produtoDeletado = _repository.Delete(produto);
            return Ok(produtoDeletado);
        }
    }
}
