using System.ComponentModel.DataAnnotations;

namespace HVM_API.Models
{
    public class Units
    {
        [Key] [StringLength(2)] public string UnitCode { get; set; }
        [StringLength(50)] public string UnitName { get; set; }
        public bool Active { get; set; }
    }
}