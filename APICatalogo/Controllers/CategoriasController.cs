using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Filters;
using APICatalogo.Repositories;
using APICatalogo.DTOs;

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

            var categoriasDto  = new List<CategoriaDTO>();
            foreach (var categoria in categorias) {

                var categoriaDto = new CategoriaDTO {

                    CategoriaId = categoria.CategoriaId,
                    Nome = categoria.Nome,
                    ImagemUrl = categoria.ImagemUrl,
                };
                categoriasDto.Add(categoriaDto);
            }

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

            var categoriaDto = new CategoriaDTO() {

                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl,
            };

            return Ok(categoriaDto);

        }

        // Adiciona categorias
        [HttpPost]
        public ActionResult<CategoriaDTO> Post (CategoriaDTO categorias) {

            if (categorias is null) {

                _logger.LogWarning($"Dados Inválidos ...");
                return BadRequest($"Dados Inválidos ...");
            }

            var categoria = new Categorias() {

                CategoriaId= categorias.CategoriaId,
                Nome = categorias.Nome,
                ImagemUrl= categorias.ImagemUrl,
            };
            
            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

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

            var categoria = new Categorias() {

                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl,
            };

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

            var categoriaExcluidaDto = new CategoriaDTO() {
                CategoriaId = categoriaExcluida.CategoriaId,
                Nome = categoriaExcluida.Nome,
                ImagemUrl = categoriaExcluida.ImagemUrl
            };

            return Ok(categoriaExcluida);
        }
    }
}
