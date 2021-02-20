using System.ComponentModel.DataAnnotations;

namespace ApplicationShared.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "You must specify password between 6 and 10 characters")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
