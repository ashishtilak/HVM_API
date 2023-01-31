using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HVM_API.Dto;
using HVM_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using AutoMapper;

namespace HVM_API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _secretKey;
        private readonly IMapper _mapper;

        //constroctor
        public LoginController(AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _secretKey = configuration["AppSettings:Secret"] ?? string.Empty;
            _context = context;
            _mapper = mapper;
        }

        //New token generation
        private string GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("username", user.UserName)
            };
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Token info getter, not required as of now
        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var token = new JwtSecurityToken(
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }


        /// <summary>
        /// Login method, accepts username and password as json object
        /// </summary>
        /// <param name="requestData">"userName" and "password" json data.</param>
        /// <returns>200 Ok with user dto with roleauths or appropriate error.</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] object requestData)
        {
            try
            {
                var dto = JsonConvert.DeserializeObject<UserDto>(requestData.ToString() ?? string.Empty);
                if (dto == null) return BadRequest("Invalid data.");

                if (string.IsNullOrEmpty(dto.UserName) ||
                    string.IsNullOrEmpty(dto.Password))
                    return BadRequest("Username/password cannot be blank.");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);
                if (user == null) return BadRequest("User not found!");
                if (dto.Password != Helper.Helper.Decode(user.Password))
                    return BadRequest("Invalid password.");

                string token = GenerateToken(dto);
                dto.Token = token;

                int[] rolesOfEmp = _context.RoleUsers
                    .Where(e => e.UserName == dto.UserName)
                    .Select(r => r.RoleId)
                    .ToArray();
            
                List<RoleAuths> roleAuths = _context.RoleAuths
                    .Where(r => r.RoleId == 1 || rolesOfEmp.Contains(r.RoleId))
                    .ToList();

                dto.RoleAuths = new List<RoleAuthsDto>();
                dto.RoleAuths.AddRange(_mapper.Map<List<RoleAuthsDto>>(roleAuths));

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }
        }
    }
}