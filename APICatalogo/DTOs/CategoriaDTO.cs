using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs; 
public class CategoriaDTO {

    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)] // DataAnnotations
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)] // DataAnnotations
    public string? ImagemUrl { get; set; }
}
