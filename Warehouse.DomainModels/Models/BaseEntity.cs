using System.ComponentModel.DataAnnotations;

namespace Warehouse.DomainModels.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
