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

        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;

        public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork uof) {

            _logger = logger;
            _uof = uof;
        }

        // Obtem todas as categorias
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categorias>> Get() {

            var categorias = _uof.CategoriaRepository.GetAll();
            return Ok(categorias);

        }

        // Obtem as categoria por ID
        [HttpGet("{id:int}", Name ="ObterCategoria")]
        public ActionResult<Categorias> Get(int id) {
            
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

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
            
            var categoriaCriada = _uof.CategoriaRepository.Create(categorias);
            _uof.Commit();

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

            var categoria = _uof.CategoriaRepository.Update(categorias);
            _uof.Commit();
            return Ok(categorias);

        }

        // Deleta as categorias
        [HttpDelete("{id:int}")]
        public ActionResult<Categorias> Delete(int id) {

            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={ id } nao encontrada ...");
                return NotFound($"Categoria com id={ id } nao encontrada ...");
            }
            
            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            return Ok(categoriaExcluida);
        }
    }
}
