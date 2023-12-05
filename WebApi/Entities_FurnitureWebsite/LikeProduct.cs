using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities_FurnitureWebsite;

public partial class LikeProduct
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
