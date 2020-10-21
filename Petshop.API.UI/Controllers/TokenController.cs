using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.DomainService;
using Petshop.Core.Enteties;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Petshop.RestAPI.UI.Helpers;

namespace Petshop.RestAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserRepository repository;

        public TokenController(IUserRepository repos)
        {
            repository = repos;
        }

        [HttpPost]
        public ActionResult Login([FromBody]LoginInputModel model)
        {
            var user = repository.GetUsers().FirstOrDefault(u => u.UserName == model.Username);
            //check if user exists
            if(user == null)
            {
                return Unauthorized("No such user found");
            }
            if(!model.Password.Equals(user.UserPassword))
            {
                return Unauthorized("Wrong password");
            }
            return Ok(new
            {
                username = user.UserName,
                token = GenerateToken(user)
            });
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            if (user.UserIsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key,
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, //issuer not needed(ValidateIssuer = false))
                               null, //audience not needed (ValidateAudience = false)
                               claims.ToArray(),
                               DateTime.Now, //not before
                               DateTime.Now.AddMinutes(5))
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
