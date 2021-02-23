using System.Collections.Generic;

namespace Warehouse.DomainModels.Models
{
    public class ShopType : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Shop> Shops { get; set; }
    }
}
