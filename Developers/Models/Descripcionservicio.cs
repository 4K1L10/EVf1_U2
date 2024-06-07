using System;
using System.Collections.Generic;

namespace Developers.Models;

public partial class Descripcionservicio
{
    public int IdDs { get; set; }

    public string? Nombre { get; set; }

    public int? IdServicio { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
