﻿using Entregas.API.Data;
using Entregas.API.Models;
using Entregas.API.Models.Domain;
using Entregas.API.Models.DTO.Cliente;
using Entregas.API.Models.Enums;
using Entregas.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Entregas.API.Repositories.Implementation
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EntregasContext context;
        private readonly UserManager<IdentityUser> userManager;
        public ClienteRepository(EntregasContext context,UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<ResponseModel> CrearCliente(CrearCliente_Request model, string userId)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                //validamos que 
                Guid id = Guid.NewGuid();
                Cuentum cuenta = new Cuentum()
                {
                    Id = id,
                    Nombre = model.nombre,
                    Apellidos = model.apellidos,
                    Correo = model.correoElectronico,
                    Telefono = model.telefono,
                    Cuenta = "",
                    Avatar = null,
                    TipoCuentaId = (int)TipoCuentaEnum.Cliente,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = Guid.Parse(userId)
                };

                IdentityUser applicationUser = new IdentityUser()
                {
                    Id = id.ToString(),
                    UserName = model.correoElectronico,
                    NormalizedUserName = model.correoElectronico.ToUpper(),
                    Email = model.correoElectronico,
                    NormalizedEmail = model.correoElectronico.ToUpper(),
                    EmailConfirmed = false,
                };

                applicationUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(applicationUser, model.password);

                await userManager.CreateAsync(applicationUser);
                await userManager.AddToRoleAsync(applicationUser, "Cliente");
                await this.context.Cuenta.AddAsync(cuenta);
                await this.context.SaveChangesAsync();

                rm.SetResponse(true, "Datos guardados con éxito.");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, ex.InnerException.Message);
            }

            return rm;
        }

        public async Task<ResponseModel> GetCliente(Guid id)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var results = await context.Cuenta.Where(x => x.Id == id && x.TipoCuentaId == (int)TipoCuentaEnum.Cliente).Select(x => new GetCliente_Result()
                {
                    id = x.Id,
                    nombre = x.Nombre,
                    apellidos = x.Apellidos,
                    telefono = x.Telefono,
                    correoElectronico = x.Correo,
                    activo = x.Activo
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

        public async Task<ResponseModel> GetClientes()
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var results = await context.Cuenta.Where(x => x.TipoCuentaId == (int)TipoCuentaEnum.Cliente).Select(x => new GetCliente_Result()
                {
                    id = x.Id,
                    nombre = x.Nombre,
                    apellidos = x.Apellidos,
                    telefono = x.Telefono,
                    correoElectronico = x.Correo,
                    activo = x.Activo
                }).ToListAsync();

                rm.result = results;
                rm.SetResponse(true);
            }
            catch (Exception)
            {
                rm.SetResponse(false);
            }

            return rm;
        }
        public async Task<ResponseModel> UpdCliente(Guid id, CrearCliente_Request model)
        {
            ResponseModel rm = new ResponseModel();
            try
            {

                var results = await context.Cuenta.Where(x => x.Id == id).ExecuteUpdateAsync(
                   s => s
                    .SetProperty(t => t.Nombre, t => model.nombre)
                    .SetProperty(t => t.Apellidos, t => model.apellidos)
                    .SetProperty(t => t.Telefono, t => model.telefono)
                    .SetProperty(t => t.Activo, t => model.activo ?? false)

                    );

                await context.SaveChangesAsync();

                rm.result = results;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }
    }
}
