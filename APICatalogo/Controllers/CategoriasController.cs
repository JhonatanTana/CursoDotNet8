using Microsoft.AspNetCore.Mvc;
using APICatalogo.Filters;
using APICatalogo.Repositories;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Pagination;
using Newtonsoft.Json;
using APICatalogo.Models;
using X.PagedList;

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

        private ActionResult<IEnumerable<CategoriaDTO>> ObterCategorias(IPagedList<Categoria> categorias) {
            var metadata = new {
                categorias.Count,
                categorias.PageSize,
                categorias.PageCount,
                categorias.TotalItemCount,
                categorias.HasNextPage,
                categorias.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);
        }

        // Obtem todas as categorias
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get() {

            var categorias = await _uof.CategoriaRepository.GetAllAsync();

            if (categorias is null)
                return NotFound("Não existem categorias...");

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);

        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriaParameters categoriaParameters) {

            var categorias = await _uof.CategoriaRepository.GetCategoriasAsync(categoriaParameters);

            return ObterCategorias(categorias);
        }

        [HttpGet("filter/nome/pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasFiltradas([FromQuery] CategoriasFiltroNome categoriasFiltro) {

            var categoriasFiltradas = await _uof.CategoriaRepository.GetCategoriasFiltroNomeAsync(categoriasFiltro);

            return ObterCategorias(categoriasFiltradas);
        }

        // Obtem as categoria por ID
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id) {

            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={id} nao encontrada ...");
                return NotFound($"Categoria com id={id} nao encontrada ...");
            }

            var categoriaDto = categoria.ToCategoriaDTO();

            return Ok(categoriaDto);

        }

        // Adiciona categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categorias) {

            if (categorias is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }

            var categoria = categorias.ToCategoria();

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            await _uof.CommitAsync();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO;

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }

        // Atualiza a categoria
        [HttpPut("{id int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDto) {

            if (categoriaDto is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
            await _uof.CommitAsync();

            var categoriaAtualizadaDto = new CategoriaDTO() {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return Ok(categoriaAtualizadaDto);

        }

        // Deleta as categorias
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id) {

            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            if (categoria is null) {

                _logger.LogWarning($"Categoria com id={id} nao encontrada ...");
                return NotFound($"Categoria com id={id} nao encontrada ...");
            }

            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            await _uof.CommitAsync();

            var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

            return Ok(categoriaExcluida);
        }
    }
}
