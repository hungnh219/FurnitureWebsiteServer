using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Permission
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
