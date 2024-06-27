using Entregas.API.Models.DTO.Cliente;
using Entregas.API.Models;
using Entregas.API.Models.DTO.Repartidor;

namespace Entregas.API.Repositories.Interface
{
    public interface IRepartidorRepository
    {
        Task<ResponseModel> CrearRepartidor(CrearRepartidor_Request model, string userId);
        Task<ResponseModel> GetRepartidores();
        Task<ResponseModel> GetRepartidor(Guid id);
        Task<ResponseModel> Update(Guid id, CrearRepartidor_Request model);
    }
}
