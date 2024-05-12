using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Filters;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : Controller {

        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger  ) {
            _context = context;
            _logger = logger;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categorias>> GetcategoriasProdutos() {

            _logger.LogInformation("================================ GET api/categorias/produtos ==================================");
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categorias>> Get() {
            
            try {

                _logger.LogInformation("================================ GET api/categorias ==================================");
                return _context.Categorias.AsNoTracking().ToList(); // rastreamento desligado

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro na solicitação");

                throw;
            }
        }

        [HttpGet("{id:int}", Name ="ObterCategoria")]
        public ActionResult<Categorias> Get(int id) {

            try {

                _logger.LogInformation($"================================ GET api/categorias/id = {id} ==================================");
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null) {

                    _logger.LogInformation($"================================ GET api/categorias/id = {id} NOT FOUND ==================================");
                    return NotFound("Categoria nao encontrada ...");
                }

                return Ok(categoria);

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Ocorreu um erro na solicitação");

                throw;
            }

        }

        [HttpPost]
        public ActionResult Post (Categorias categorias) {

            try {

                if (categorias == null) {
                    return BadRequest();
                }

                _context.Categorias.Add(categorias);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categorias.CategoriaId }, categorias);

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Ocorreu um erro na solicitação");

                throw;
            }
        }

        [HttpPut("{id int}")]
        public ActionResult Put(int id, Categorias categorias) {

            try {

                if (id != categorias.CategoriaId) {
                    return BadRequest();
                }

                _context.Entry(categorias).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categorias);

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Ocorreu um erro na solicitação");

                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categorias> Delete(int id) {

            try {

                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null) {
                    return NotFound();
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

                return Ok(categoria);

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Ocorreu um erro na solicitação");

                throw;
            }
        }
    }
}
