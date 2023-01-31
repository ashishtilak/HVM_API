using System.ComponentModel.DataAnnotations;

namespace HVM_API.Models
{
    public class AuthObjects
    {
        [Key] public int Id { get; set; }
        [StringLength(50)]
        public string AuthObjName { get; set; }
        [StringLength(100)]
        public string AuthObjDesc { get; set; }
    }
}
