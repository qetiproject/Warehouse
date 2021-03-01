namespace ApplicationShared.DTOs.Product
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturingCompany { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
