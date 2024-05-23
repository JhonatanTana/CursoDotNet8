using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Categorias")] //DataAnnotations
public class Categoria {

    public Categoria() {
        Produtos = new Collection<Produto>();
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
    public ICollection<Produto>? Produtos { get; set; }  // Define uma chave estrangeira
}
