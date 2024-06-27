using Entregas.API.Helpers;
using Entregas.API.Models.DTO.Cliente;
using Entregas.API.Models.DTO.Repartidor;
using Entregas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entregas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepartidorController : ControllerBase
    {
        private readonly IRepartidorRepository repartidorRepository;
        public RepartidorController(IRepartidorRepository repartidorRepository)
        {
            this.repartidorRepository = repartidorRepository;
        }

        [HttpPost]
        [Route("CrearRepartidor")]
        [Authorize]
        public async Task<IActionResult> CrearRepartidor([FromBody] CrearRepartidor_Request request)
        {
            var response = await repartidorRepository.CrearRepartidor(request, User.GetId());
            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }
            return Ok(response.result);
        }

        [HttpGet]
        [Route("GetRepartidores")]
        [Authorize]
        public async Task<IActionResult> GetRepartidores()
        {
            var response = await repartidorRepository.GetRepartidores();
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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CrearRepartidor_Request request)
        {

            var response = await repartidorRepository.Update(id, request);

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
