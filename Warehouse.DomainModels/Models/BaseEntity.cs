using System.ComponentModel.DataAnnotations;

namespace Warehouse.DomainModels.Models
{
    public class IdentityUser
    {
        [Key]
        public int Id { get; set; }
    }
}
