using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HVM_API.Models
{
    public class RoleAuths
    {
        [Key, Column(Order = 0)] public int RoleId {get;set;}
        [ForeignKey("RoleId")] public Roles Role {get;set;}

        [Key, Column(Order = 1)] public int AuthObjId {get;set;}
        [ForeignKey("AuthObjId")] public AuthObjects AuthObject {get;set;}

        public bool Active {get;set;}

    }
}
