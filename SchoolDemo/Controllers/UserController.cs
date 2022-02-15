using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDemo.Models;
using System.Linq;
using System.Security.Claims;

namespace SchoolDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       // [HttpGet("Administrator")]
        //[Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi{currentUser.GivenName},you are an{currentUser.Role}"); 
        }
        [HttpGet("Seller")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser.GivenName},you are an {currentUser.Role}");
        }
        [HttpGet("AdminsAndSeller")]
        [Authorize(Roles = "Adminstrator,Sellers")]
        public IActionResult AdminsAndSellerEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi{currentUser.GivenName},you are an{currentUser.Role}");
        }

        [HttpGet("public")]
        public IActionResult Public()
       {
            return Ok("Hi you're on public property");
       }
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }

    }
}
