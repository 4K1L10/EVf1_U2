using System;
using System.Collections.Generic;

namespace Developers.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string? Nombre { get; set; }

    public int? Precio { get; set; }

    public string? Sku { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<Descripcionservicio> Descripcionservicios { get; set; } = new List<Descripcionservicio>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Recepcionequipo> Recepcionequipos { get; set; } = new List<Recepcionequipo>();
}
