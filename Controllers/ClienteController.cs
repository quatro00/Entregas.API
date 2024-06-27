using Entregas.API.Helpers;
using Entregas.API.Models;
using Entregas.API.Models.DTO.Auth;
using Entregas.API.Models.DTO.Cliente;
using Entregas.API.Models.DTO.Cuenta;
using Entregas.API.Repositories.Implementation;
using Entregas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Entregas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        [HttpPost]
        [Route("CrearCliente")]
        [Authorize]
        public async Task<IActionResult> CrearCliente([FromBody] CrearCliente_Request request)
        {
            var response = await clienteRepository.CrearCliente(request, User.GetId());
            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }
            return Ok(response.result);
        }

        [HttpGet]
        [Route("GetClientes")]
        [Authorize]
        public async Task<IActionResult> GetClientes()
        {
            var response = await clienteRepository.GetClientes();
            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }
            return Ok(response.result);
        }

        
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CrearCliente_Request request)
        {
            
            var response = await clienteRepository.UpdCliente(id, request);

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }
            
            return Ok(response.result);
        }
         
    }
}
