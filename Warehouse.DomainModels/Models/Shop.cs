using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.DomainModels.Models
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        [ForeignKey("ShopTypeId")]
        public int ShopTypeId { get; set; }
        public ShopType Shoptype { get; set; }
        public bool IsDeleted { get; set; }
    }
}
