namespace HVM_API.Dto
{
    public class RolesDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }

        public List<RoleAuthsDto> RoleAuths { get; set; }
        public List<RoleUsersDto> RoleUsers { get; set; }
    }
}
