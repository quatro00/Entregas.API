using Entregas.API.Data;
using Entregas.API.Models;
using Entregas.API.Models.DTO.Cuenta;
using Entregas.API.Repositories.Interface;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Entregas.API.Repositories.Implementation
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly EntregasContext context;
        public CuentaRepository(EntregasContext context) => this.context = context;
        public async Task<ResponseModel> GetById(Guid id)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                var results = await context.Cuenta
                    .Where(x => x.Id == id)
                    .Select(x => new GetCuenta_Response()
                    {
                        id = x.Id.ToString(),
                        nombre = x.Nombre,
                        apellidos = x.Apellidos,
                        correo = x.Correo,
                        telefono = x.Telefono,
                        cuenta = x.Cuenta,
                        tipoCuentaId = x.TipoCuentaId,
                        avatar = x.Avatar,
                    }).FirstOrDefaultAsync();

                rm.result = results;
                rm.SetResponse(true);
            }
            catch (Exception)
            {
                rm.SetResponse(false);
            }
            return rm;
        }
    }
}
