using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using VetSystems.Identity.Infrastructure.Entities;

namespace VetSystems.IdentityServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //[HttpPost]
        //public async Task<IActionResult> SignUp(SignupDto signupDto)
        //{
        //    var user = new ApplicationUser
        //    {
        //        UserName = signupDto.UserName,
        //        Email = signupDto.Email,    
        //    };
        //    var result = await _userManager.CreateAsync(user, signupDto.Password);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(result);
        //}


        [HttpPost]
        public async Task<IActionResult> SignIn()
        {

            return Ok();
        }


    }
}
