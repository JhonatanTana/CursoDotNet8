using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;
[Table("Produto")] // DataAnnotations
public class Produto {

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
    public DateTime DataCadatro { get; set; }
    public int CategoriaId { get; set; } // Relaciona a chave estrangeira
    public Categoria? Categoria { get; set; }  // Relaciona a chave estrangeira
}