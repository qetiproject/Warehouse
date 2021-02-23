using Microsoft.AspNetCore.Identity;

namespace ApplicationShared.DTOs
{
    public class UserDto : IdentityUser
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}