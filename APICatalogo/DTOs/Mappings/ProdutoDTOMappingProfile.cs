using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTOs.Mappings;
public class ProdutoDTOMappingProfile : Profile {

    public ProdutoDTOMappingProfile() {

        CreateMap<Produtos, ProdutoDTO>().ReverseMap();
        CreateMap<Categorias, CategoriaDTO>().ReverseMap();
    }
}
