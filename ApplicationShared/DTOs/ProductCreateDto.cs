using System.ComponentModel.DataAnnotations;

namespace ApplicationShared.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ManufacturingCompany { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Barcode { get; set; }
    }
}
