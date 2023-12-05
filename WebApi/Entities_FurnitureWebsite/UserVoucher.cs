using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class UserVoucher
{
    public int UserId { get; set; }

    public int VoucherId { get; set; }

    public int? Amount { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
