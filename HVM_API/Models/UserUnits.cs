using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HVM_API.Models
{
    public class UserUnits
    {
        [Key, Column(Order = 0)] [StringLength(20)] public string UserName { get; set; }
        [ForeignKey("UserName")] public Users User { get; set; }

        [Key, Column(Order = 1)] [StringLength(2)] public string UnitCode {get;set;}
        [ForeignKey("UnitCode")] public Units Unit { get; set; }

        [StringLength(20)] public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
