using Entregas.API.Models;
using Entregas.API.Models.DTO.Cliente;

namespace Entregas.API.Repositories.Interface
{
    public interface IClienteRepository
    {
        Task<ResponseModel> CrearCliente(CrearCliente_Request model, string userId);
        Task<ResponseModel> GetClientes();
        Task<ResponseModel> GetCliente(Guid id);
        Task<ResponseModel> UpdCliente(Guid id, CrearCliente_Request model);
    }
}
