using APICatalogo.Models;
using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.DTOs; 
public class ProdutoDTO {

    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)] // DataAnnotations
    [PrimeiraLetraMaiuscula] // Validaçao personalizada
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)] // DataAnnotations
    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)] // DataAnnotations
    public string? ImagemUrl { get; set; }
    public int CategoriaId { get; set; } // Relaciona a chave estrangeira


}
