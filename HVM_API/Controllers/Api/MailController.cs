using HVM_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HVM_API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MailController(AppDbContext context)
        {
            _context=context;
        }
    }
}
