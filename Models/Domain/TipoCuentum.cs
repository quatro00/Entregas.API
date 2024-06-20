using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class TipoCuentum
{
    public int TipoCuentaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
