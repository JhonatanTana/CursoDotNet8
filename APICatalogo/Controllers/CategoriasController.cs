using Microsoft.AspNetCore.Mvc;
using APICatalogo.Filters;
using APICatalogo.Repositories;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;

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
        public ActionResult<IEnumerable<CategoriaDTO>> Get() {

            var categorias = _uof.CategoriaRepository.GetAll();

            var categoriasDto = categorias.ToCategoriaDTOList;

            return Ok(categoriasDto);

        }

        // Obtem as categoria por ID
        [HttpGet("{id:int}", Name ="ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id) {
            
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={ id } nao encontrada ...");
                return NotFound($"Categoria com id={ id } nao encontrada ...");
            }

            var categoriaDto = categoria.ToCategoriaDTO();

            return Ok(categoriaDto);

        }

        // Adiciona categorias
        [HttpPost]
        public ActionResult<CategoriaDTO> Post (CategoriaDTO categorias) {

            if (categorias is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }

            var categoria = categorias.ToCategoria();
            
            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO;

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        // Atualiza a categoria
        [HttpPut("{id int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto) {
            
            if (categoriaDto is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            var categoriaAtualizadaDto = new CategoriaDTO() {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return Ok(categoriaAtualizadaDto);

        }

        // Deleta as categorias
        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id) {

            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={ id } nao encontrada ...");
                return NotFound($"Categoria com id={ id } nao encontrada ...");
            }
            
            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

            return Ok(categoriaExcluida);
        }
    }
}
