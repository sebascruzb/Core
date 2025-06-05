using System;
using System.Collections.Generic;

namespace MiniCore.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
