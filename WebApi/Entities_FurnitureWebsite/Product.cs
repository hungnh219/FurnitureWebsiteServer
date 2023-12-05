using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Info { get; set; }

    public int? Price { get; set; }

    public string? Material { get; set; }

    public double? Width { get; set; }

    public double? Height { get; set; }

    public double? Depth { get; set; }

    public double? Weight { get; set; }

    public string? ImgDirect { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<LikeProduct> LikeProducts { get; set; } = new List<LikeProduct>();

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
