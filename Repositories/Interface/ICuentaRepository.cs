using Entregas.API.Models;

namespace Entregas.API.Repositories.Interface
{
    public interface ICuentaRepository
    {
        Task<ResponseModel> GetById(Guid id);
    }
}
