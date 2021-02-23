using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.DomainModels.Models
{
    public class ProductInShop : BaseEntity
    {
        [ForeignKey("ShopId")]
        public int ShopId { get; set; }
        public virtual List<Shop> Shops { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public virtual List<Product> Product { get; set; }
    }
}
