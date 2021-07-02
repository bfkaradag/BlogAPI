using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        
        
        public AdminController(IJwtAuthenticationManager jwtAuthenticationManager, IConfiguration _configuration)
        {
           this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // GET: api/<AdminController>
        [HttpPost("addpost")]
        public IActionResult AddPost([FromBody] BlogPost post)
        {
            int result = IOC.adminService.AddPost(post);
            if (result > 0)
                return Ok();
            return NotFound();

        }
        [HttpDelete("deletepost/{id}")]
        public IActionResult DeletePost(int id) {
            int result = IOC.adminService.DeletePost(id);
            if (result > 0)
                return Ok();
            return NotFound();
        }

        [HttpPost("updatepost")]
        public IActionResult UpdatePost([FromBody] BlogPost post)
        {
            int result = IOC.adminService.UpdatePost(post);
            if (result > 0)
                return Ok();
            return NotFound();
        }

        // GET api/<AdminController>/5
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred usr)
        {
            var token = jwtAuthenticationManager.Authenticate(usr.username, usr.password);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        } 
    }
}
