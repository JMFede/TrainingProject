using System;
using System.Collections.Generic;

namespace TrainingProject.Models;

public partial class Line
{
    public int LineId { get; set; }

    public string? LineName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
