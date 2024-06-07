using System;
using System.Collections.Generic;

namespace Ecommerce.Modelo;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public string? Rol { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
