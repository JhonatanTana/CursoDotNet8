using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Filters;
using APICatalogo.Repositories;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : Controller {

        private readonly ICategoriaRepository _repository;
        private readonly ILogger _logger;

        public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> logger  ) {
            _repository = repository;
            _logger = logger;
        }

        // Obtem todas as categorias
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categorias>> Get() {

            var categorias = _repository.GetCategorias();
            return Ok(categorias);

        }

        // Obtem as categoria por ID
        [HttpGet("{id:int}", Name ="ObterCategoria")]
        public ActionResult<Categorias> Get(int id) {
            
            var categoria = _repository.GetCategorias(id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={ id } nao encontrada ...");
                return NotFound($"Categoria com id={ id } nao encontrada ...");
            }

            return Ok(categoria);

        }

        // Adiciona categorias
        [HttpPost]
        public ActionResult Post (Categorias categorias) {

            if (categorias is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }
            
            var categoriaCriada = _repository.Create(categorias);

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        // Atualiza a categoria
        [HttpPut("{id int}")]
        public ActionResult Put(int id, Categorias categorias) {
            
            if (categorias is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }

            var categoria = _repository.Update(categorias);
            return Ok(categorias);

        }

        // Deleta as categorias
        [HttpDelete("{id:int}")]
        public ActionResult<Categorias> Delete(int id) {

            var categoria = _repository.GetCategorias(id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={ id } nao encontrada ...");
                return NotFound($"Categoria com id={ id } nao encontrada ...");
            }
            
            var categoriaExcluida = _repository.Delete(id);
            return Ok(categoriaExcluida);
        }
    }
}
