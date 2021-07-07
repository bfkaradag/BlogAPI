using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("getposts/{page}")]
        public List<BlogPost> GetPosts(int page)
        {
            List<BlogPost> lst = IOC.userService.GetPostsByLazyLoad(page);
            return lst;
        }

        [HttpGet("getpostlength")]
        public IActionResult GetPostsLength()
        {
            int result = IOC.userService.GetPostsLength();
            if (result > 0)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("getpostsbytagname/{txt}")]
        public List<BlogPost> GetPostsByTagName(string txt)
        {
            return IOC.userService.GetPostBySearchTagName(txt);
        }
    }
}
