using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : Controller {

        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository) {
            _repository = repository;
        }

        [HttpGet]  // Retorna os produtos
        public ActionResult<IEnumerable<Produtos>> Get() {

            var produtos = _repository.GetProdutos().ToList();

            if (produtos is null) {
                return NotFound("Produstos não encontrados ...");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")] // Retorna um produto especificado pelo ID
        public ActionResult<Produtos> Get(int id) { 

            var produtos = _repository.GetProdutos(id);

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

            bool atualizado = _repository.Update(produto);

            if (atualizado) {

                return Ok(produto);
            } else {

                return StatusCode(500, $"Falha ao atualiza o produto {Id}");
            }
        }

        [HttpDelete("{id:int}")] // Deleta o produto pelo ID
        public ActionResult Delete(int id) {
            
            bool deletado = _repository.Delete(id);

            if (deletado) { 
                
                return Ok($"O produto com { id } foi excluido ...");
            }else {

                return StatusCode(500, $"Falha ao excluir o produto { id }");
            }
        }
    }
}
