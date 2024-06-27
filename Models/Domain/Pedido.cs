using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class Pedido
{
    public Guid Id { get; set; }

    public int Folio { get; set; }

    public Guid CuentaId { get; set; }

    public Guid? RepartidorId { get; set; }

    public int EstatusPedidoId { get; set; }

    public Guid LocalId { get; set; }

    public string Destino { get; set; } = null!;

    public string Observaciones { get; set; } = null!;

    public string? DestinoLat { get; set; }

    public string? DestinoLon { get; set; }

    public decimal? Distancia { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Comision { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaAsignado { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public DateTime? FechaCancelacion { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacionId { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public Guid? UsuarioModificacionId { get; set; }

    public virtual EstatusPedido EstatusPedido { get; set; } = null!;

    public virtual Local Local { get; set; } = null!;

    public virtual Cuentum? Repartidor { get; set; }
}
