using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Revenue
{
    public int Year { get; set; }

    public int Month { get; set; }

    public int? Value { get; set; }
}
