using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Colour
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Barcode { get; set; }

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
