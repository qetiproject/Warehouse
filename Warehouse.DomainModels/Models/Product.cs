namespace Warehouse.DomainModels.Models
{
    public class Product:IdentityUser
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string ManufacturingCompany { get; set; }
        public double Price { get; set; }
        public int Barcode { get; set; }
        public bool IsDeleted { get; set; }
    }
}
