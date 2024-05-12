using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;
[Table("Produtos")] // DataAnnotations
public class Produtos : IValidatableObject {

    [Key] // DataAnnotations
    public int ProdutoId { get; set; }
    
    [Required]
    [StringLength(80)] // DataAnnotations
    [PrimeiraLetraMaiuscula] // Validaçao personalizada
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
        
        if(!string.IsNullOrEmpty(this.Nome)) {

            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper()) {

                yield return new
                    ValidationResult("A primeira letra deve ser maiuscula",
                    new[]
                    { nameof(this.Nome) });
            }

            if (this.Estoque <= 0) {

                yield return new
                    ValidationResult("o estoque deve ser maior que 0",
                    new[]
                    { nameof(this.Estoque) }
                    );
            }
        }
    }
}