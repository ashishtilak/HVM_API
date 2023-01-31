using HVM_API.Models;

namespace HVM_API.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Token { get; set; }

        public List<RoleAuthsDto> RoleAuths { get; set; }
        public List<UserUnitsDto> UserUnits { get; set; }
    }
}