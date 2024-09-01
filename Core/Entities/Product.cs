using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Product : BaseEntity
{
    [Required] [MaxLength(255)] public required string Name { get; set; }

    public required string Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")] public decimal Price { get; set; }

    public string? PictureUrl { get; set; }
    public required string Type { get; set; }
    public required string Brand { get; set; }
    public int QuantityInStock { get; set; }
}