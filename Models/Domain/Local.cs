using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class Local
{
    public Guid LocalId { get; set; }

    public Guid CuentaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Lat { get; set; } = null!;

    public string Lon { get; set; } = null!;

    public string? Avatar { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public Guid? UsuarioModificacion { get; set; }

    public virtual Cuentum Cuenta { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
