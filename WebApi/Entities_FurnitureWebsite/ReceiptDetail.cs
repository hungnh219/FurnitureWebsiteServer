using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class ReceiptDetail
{
    public int ReceiptId { get; set; }

    public int ProductId { get; set; }

    public int? Amount { get; set; }

    public int ColourId { get; set; }

    public virtual Colour Colour { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Receipt Receipt { get; set; } = null!;
}
