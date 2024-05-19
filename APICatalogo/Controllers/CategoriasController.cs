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

            _logger.LogInformation("================================ GET api/categorias ==================================");
            return _context.Categorias.AsNoTracking().ToList(); // rastreamento desligado

        }

        [HttpGet("{id:int}", Name ="ObterCategoria")]
        public ActionResult<Categorias> Get(int id) {
            
            _logger.LogInformation($"================================ GET api/categorias/id = {id} ==================================");
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null) {

                _logger.LogInformation($"================================ GET api/categorias/id = {id} NOT FOUND ==================================");
                return NotFound("Categoria nao encontrada ...");
            }

            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult Post (Categorias categorias) {


            if (categorias == null) {
                return BadRequest();
            }

            _context.Categorias.Add(categorias);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categorias.CategoriaId }, categorias);

        }

        [HttpPut("{id int}")]
        public ActionResult Put(int id, Categorias categorias) {

            if (id != categorias.CategoriaId) {
                return BadRequest();
            }

            _context.Entry(categorias).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categorias);

        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categorias> Delete(int id) {

            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null) {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);

        }
    }
}
