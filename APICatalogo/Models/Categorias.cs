using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public ICollection<Produtos>? Produtos { get; set; }  // Define uma chave estrangeira
}
