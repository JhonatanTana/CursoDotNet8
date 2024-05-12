using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;
[Table("Produtos")] // DataAnnotations
public class Produtos {

    [Key] // DataAnnotations
    public int ProdutoId { get; set; }
    
    [Required]
    [StringLength(80)] // DataAnnotations
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)] // DataAnnotations
    public string? Descricao { get; set; }
    
    [Required]
    [Column (TypeName = "decimal(10,2)")] // DataAnnotations
    public decimal Preco { get; set; }
    
    [Required]
    [StringLength(300)] // DataAnnotations
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; } // Relaciona a chave estrangeira
    [JsonIgnore]
    public Categorias? Categoria { get; set; }  // Relaciona a chave estrangeira
}