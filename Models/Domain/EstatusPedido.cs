using System;
using System.Collections.Generic;

namespace Entregas.API.Models.Domain;

public partial class EstatusPedido
{
    public int EstatusPedidoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
