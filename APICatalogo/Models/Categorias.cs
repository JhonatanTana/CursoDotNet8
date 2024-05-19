using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Categorias")] //DataAnnotations
public class Categorias {

    public Categorias() {
        Produtos = new Collection<Produtos>();
    }

    [Key] // DataAnnotations
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)] // DataAnnotations
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)] // DataAnnotations
    public string? ImagemUrl { get; set; }

    [JsonIgnore] // DataAnnotations
    public ICollection<Produtos>? Produtos { get; set; }  // Define uma chave estrangeira
}
