using System.ComponentModel;
using System.Security.Claims;
using AutoMapper;
using HVM_API.Dto;
using HVM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HVM_API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<List<UserDto>> GetUserDetails(bool onlyActive = false, string username = "")
        {
            IQueryable<Users> query = _context.Users;

            if (onlyActive)
                query = query.Where(u => u.Active);

            if (username != "")
                query = query.Where(u => u.UserName == username);

            var users = await query.ToListAsync();

            if (users.Count == 0) throw new Exception("No users found!");

            var userDtos = _mapper.Map<List<UserDto>>(users);
            var userList = userDtos.Select(u => u.UserName).Distinct();

            var roleUsers = await _context.RoleUsers.Where(u => userList.Contains(u.UserName)).ToListAsync();
            if (roleUsers.Count > 0)
            {
                var roleIds = roleUsers.Select(r => r.RoleId).Distinct();
                var roleAuths = await _context.RoleAuths.Where(r => roleIds.Contains(r.RoleId)).ToListAsync();
                if (roleAuths.Count > 0)
                {
                    foreach (UserDto dto in userDtos)
                    {
                        var userRole = roleUsers.Where(r => r.UserName == dto.UserName).Select(r => r.RoleId)
                            .Distinct();
                        var userAuths = roleAuths.Where(r => userRole.Contains(r.RoleId)).ToList();
                        dto.RoleAuths = new List<RoleAuthsDto>();
                        dto.RoleAuths.AddRange(_mapper.Map<List<RoleAuthsDto>>(userAuths));
                    }
                }
            }

            return userDtos;
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(bool onlyActive = false)
        {
            try
            {
                return Ok(await GetUserDetails(onlyActive));
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(string username, bool onlyActive = false)
        {
            try
            {
                return Ok(await GetUserDetails(onlyActive, username));
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] object requestData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = identity?.FindFirst("username")?.Value ?? "";
            if (user == "") return BadRequest("Invalid user in token.");

            var dto = JsonConvert.DeserializeObject<UserDto>(requestData.ToString() ?? string.Empty);
            if (dto == null) return BadRequest("Invalid data passed.");
            if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password) ||
                string.IsNullOrEmpty(dto.Email))
                return BadRequest("Provide username, password, and email.");

            dto.UserName = dto.UserName.Trim();

            try
            {
                //check if already exists...
                var already = await _context.Users.FirstOrDefaultAsync(u=>u.UserName == dto.UserName);
                if(already != null)
                    return BadRequest("User already exist.");

                Users newUser = _context.Users.Add(new Users
                {
                    UserName = dto.UserName,
                    Password = Helper.Helper.Encode(dto.Password),
                    Email = dto.Email,
                    Active = true,
                }).Entity;

                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<UserDto>(newUser));
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeUser([FromBody] object requestData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = identity?.FindFirst("username")?.Value ?? "";
            if (username == "") return BadRequest("Invalid user in token.");

            var dto = JsonConvert.DeserializeObject<UserDto>(requestData.ToString() ?? string.Empty);
            if (dto == null) return BadRequest("Invalid data passed.");
            if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password) ||
                string.IsNullOrEmpty(dto.Email))
                return BadRequest("Provide username, password, and email.");

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserName == dto.UserName);
                if (user == null) return BadRequest("User not found.");

                user.Email =dto.Email;
                user.Password = dto.Password;

                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }
        }
    }
}