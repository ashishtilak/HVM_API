using System.ComponentModel.DataAnnotations;

namespace HVM_API.Models
{
    public class Roles
    {
        [Key] public int Id { get; set; }
        [StringLength(50)] public string RoleName { get; set; }
        public bool Active { get; set; }
    }
}