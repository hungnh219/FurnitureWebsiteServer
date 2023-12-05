using System;
using System.Collections.Generic;

namespace WebApi.Entities_FurnitureWebsite;

public partial class Argument
{
    public int? RegAge { get; set; }

    public decimal? MinPaid { get; set; }

    public double? RatingUpper { get; set; }

    public double? RatingLower { get; set; }
}
