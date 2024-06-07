using System;
using System.Collections.Generic;

namespace Developers.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
  
}
