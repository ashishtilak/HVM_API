using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HVM_API.Models
{
    public class RoleUsers
    {
        [Key, Column(Order = 0)] [StringLength(20)] public string UserName { get; set; }
        [ForeignKey("UserName")] public Users User { get; set; }
        [Key, Column(Order = 1)] public int RoleId { get; set; }
        [ForeignKey("RoleId")] public Roles Role { get; set; }

        [StringLength(20)] public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
