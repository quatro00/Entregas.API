using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class Pago
{
    public Guid Id { get; set; }

    public int Folio { get; set; }

    public DateTime Fecha { get; set; }

    public Guid? CuentaId { get; set; }

    public int ConceptoPagoId { get; set; }

    public decimal Importe { get; set; }

    public string Observaciones { get; set; } = null!;

    public int EstatusPagoId { get; set; }

    public int MetodoPagId { get; set; }

    public string Comprobante { get; set; } = null!;

    public string Referencia { get; set; } = null!;

    public bool Actvo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacionId { get; set; }

    public DateTime FechaModificacion { get; set; }

    public Guid UsuarioModificacionId { get; set; }

    public virtual ConceptoPago ConceptoPago { get; set; } = null!;

    public virtual Cuentum? Cuenta { get; set; }

    public virtual MetodoPago MetodoPag { get; set; } = null!;
}
