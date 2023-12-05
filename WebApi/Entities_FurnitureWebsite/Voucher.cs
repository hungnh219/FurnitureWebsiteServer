using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Voucher
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public decimal? Value { get; set; }

    public DateTime? ValidDate { get; set; }

    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
