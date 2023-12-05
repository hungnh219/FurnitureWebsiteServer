using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Comment
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public double? Rating { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
