using System;
using System.Collections.Generic;

namespace MVC1.Models;

public partial class PriceChangeLog
{
    public int LogId { get; set; }

    public int ProductId { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public DateTime ChangedAt { get; set; }
}
