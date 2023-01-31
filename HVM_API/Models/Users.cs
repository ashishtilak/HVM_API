using System.ComponentModel.DataAnnotations;

namespace HVM_API.Models
{
    public class Users
    {
        [Key] [StringLength(20)] public string UserName { get; set; }
        [Required] [StringLength(20)] public string Password { get; set; }
        [StringLength(255)] public string Email { get; set; }
        public bool Active { get; set; }
    }
}