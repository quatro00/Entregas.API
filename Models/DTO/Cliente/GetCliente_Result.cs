namespace Entregas.API.Models.DTO.Cliente
{
    public class GetCliente_Result
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correoElectronico { get; set; }
        public string telefono { get; set; }
        public bool activo { get; set; }
    }
}
