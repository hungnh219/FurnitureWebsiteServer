using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? PassWord { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public DateTime? RegDate { get; set; }

    public int? BirthYear { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<LikeProduct> LikeProducts { get; set; } = new List<LikeProduct>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<UserVoucher> UserVouchers { get; set; } = new List<UserVoucher>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
