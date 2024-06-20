using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Entregas.API.Repositories.Interface;
using Entregas.API.Data;
using Entregas.API.Models.DTO.Auth;
using Entregas.API.Models.DTO.Cuenta;

namespace Entregas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ICuentaRepository cuentaRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, RoleManager<ApplicationRole> roleManager, ICuentaRepository cuentaRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.roleManager = roleManager;
            this.cuentaRepository = cuentaRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            int sistemaId = 1;
            //checamos el email
            var identityUser = await userManager.FindByNameAsync(request.username);

            if (identityUser is not null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.password);

                if (checkPasswordResult)
                {

                    var roles = await userManager.GetRolesAsync(identityUser);
                    List<string> rolesFinal = new List<string>();


                    foreach (var item in roles)
                    {
                        var rols_ = await this.roleManager.FindByNameAsync(item);
                        if (rols_.SistemaId == sistemaId)
                        {
                            rolesFinal.Add(rols_.Name ?? "");
                        }
                    }

                    if (rolesFinal == null)
                    {
                        ModelState.AddModelError("error", "Email o password incorrecto.");
                        return ValidationProblem(ModelState);
                    }

                    if (rolesFinal.Count == 0)
                    {
                        ModelState.AddModelError("error", "Email o password incorrecto.");
                        return ValidationProblem(ModelState);
                    }


                    //buscamos la cuenta
                    var cuentaResult = await cuentaRepository.GetById(Guid.Parse(identityUser.Id));
                    GetCuenta_Response cuenta = cuentaResult.result;

                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto()
                    {
                        Email = identityUser.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken,
                        Nombre = cuenta.nombre,
                        Apellidos = cuenta.apellidos,
                        Username = identityUser.UserName
                    };

                    return Ok(response);
                }

            }

            ModelState.AddModelError("error", "Email o password incorrecto.");
            return ValidationProblem(ModelState);
        }
    }
}
