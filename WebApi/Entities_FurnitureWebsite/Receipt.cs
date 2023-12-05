using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Receipt
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int? Paid { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual User? User { get; set; }

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
