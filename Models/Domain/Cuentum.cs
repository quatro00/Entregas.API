using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class Cuentum
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Cuenta { get; set; } = null!;

    public string? Avatar { get; set; }

    public int TipoCuentaId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public Guid? UsuarioModificacion { get; set; }

    public virtual ICollection<Local> Locals { get; set; } = new List<Local>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual TipoCuentum TipoCuenta { get; set; } = null!;
}
