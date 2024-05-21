using APICatalogo.Models;

namespace APICatalogo.DTOs.Mappings; 
public static class CategoriaDTOMappingExtensions {

    public static CategoriaDTO? ToCategoriaDTO (this Categorias categoria) {

        if (categoria is null) {
            
            return null;
        }

        return new CategoriaDTO {

            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl,
        };
    }

    public static Categorias? ToCategoria(this CategoriaDTO categoriaDto) { 
        
        if(categoriaDto is null) {
            return null;
        }

        return new Categorias {

            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl,
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categorias> categorias) { 
        
        if (categorias is null || !categorias.Any()) {

            return new List<CategoriaDTO>();
        }

        return categorias.Select(categoria => new CategoriaDTO {

            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl,
        }).ToList();
    }
}
