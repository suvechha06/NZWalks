using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST : /api/Auth/Register  -> For new user registration
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if(identityResult.Succeeded)
            {
                //Add roles for the user
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User Registered Successfully. Please Login!");
                    }
                }                
            }
            return BadRequest("Something went wrong.");
        }

        //POST : api/Auth/Login -> For existing user login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                
                if(checkPassword)
                {
                    //Get Roles for the user
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO()
                        {
                            JwtToken = jwtToken
                        };
                        
                        return Ok(response);
                    }           
                }
            } 
            return BadRequest("Incorrect Credentials");
        }
    }
}
