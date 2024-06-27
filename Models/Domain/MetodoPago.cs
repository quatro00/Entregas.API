using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class MetodoPago
{
    public int MetodoPagoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
