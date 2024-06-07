using System;
using System.Collections.Generic;

namespace Developers.Models;

public partial class Recepcionequipo
{
    public int IdRe { get; set; }

    public int? IdServicio { get; set; }

    public int? IdCliente { get; set; }

    public DateTime? Fecha { get; set; }

    public int? TipoPc { get; set; }

    public string? Accesorio { get; set; }

    public string? MarcaPc { get; set; }

    public string? MoledoPc { get; set; }

    public string? Nserie { get; set; }

    public int? CanpacidadRam { get; set; }

    public string? TipoAlmacenamiento { get; set; }

    public int? TipoGpu { get; set; }

    public string? Grafico { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
