﻿namespace Entregas.API.Models.DTO.Repartidor
{
    public class CrearRepartidor_Request
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string password { get; set; }
        public string correoElectronico { get; set; }
        public bool? activo { get; set; }
    }
}
