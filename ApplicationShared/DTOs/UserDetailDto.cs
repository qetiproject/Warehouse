using Warehouse.DomainModels.Models;

namespace ApplicationShared.DTOs
{
    public class UserDetailDto : BaseEntity
    {
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
