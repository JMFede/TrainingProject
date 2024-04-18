using System;
using System.Collections.Generic;

namespace TrainingProject.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string? Name { get; set; }

    public int? TypeId { get; set; }

    public int? LineId { get; set; }

    public string? Batch { get; set; }

    public int? Quantity { get; set; }

    public int? PlannedQuantity { get; set; }

    public string? PlannedDate { get; set; }

    public int? UserId { get; set; }

    public string? Wbs { get; set; }

    public string? StartingDate { get; set; }

    public int? StatusId { get; set; }

    public virtual Line? Line { get; set; }

    public virtual OrderStatus? Status { get; set; }

    public virtual Type? Type { get; set; }

    public virtual User? User { get; set; }
}
