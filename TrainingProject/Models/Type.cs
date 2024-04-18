using System;
using System.Collections.Generic;

namespace TrainingProject.Models;

public partial class Type
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
