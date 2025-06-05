using System;
using System.Collections.Generic;

namespace MiniCore.Models;

public partial class Tarea
{
    public int Id { get; set; }

    public string Nombredelatarea { get; set; } = null!;

    public DateOnly? FechadeInicio { get; set; }

    public double? TiempoEstimado { get; set; }

    public string? EstadoProgreso { get; set; }

    public int? ProyectoId { get; set; }

    public int? EmpleadoId { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual Proyecto? Proyecto { get; set; }
}
