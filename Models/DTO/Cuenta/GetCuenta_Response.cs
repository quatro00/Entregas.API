using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Entregas.API.Models.DTO.Cuenta
{
    public class GetCuenta_Response
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string cuenta { get; set; }
        public string avatar { get; set; }
        public int tipoCuentaId { get; set; }

    }
}
