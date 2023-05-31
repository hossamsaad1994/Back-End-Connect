using System.ComponentModel.DataAnnotations;

namespace connect_.Models
{
    public class RegisterationModel
    {
        [Required, MaxLength(100)]
        public string Firstname { get; set; }
        [Required, MaxLength(100)]
        public string Lastname { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(128)]
        public string Email { get; set; }
        [Required, MaxLength(256)]
        public string Password { get; set; }
    }
}
