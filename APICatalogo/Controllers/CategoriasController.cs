using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : Controller {

        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext context) {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categorias>> GetcategoriasProdutos() {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categorias>> Get() {
            
            try {

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

                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null) {
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
