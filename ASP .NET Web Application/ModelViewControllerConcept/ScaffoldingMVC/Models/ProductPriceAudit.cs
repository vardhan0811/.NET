using System;
using System.Collections.Generic;

namespace ScaffoldingMVC.Models;

public partial class ProductPriceAudit
{
    public int AuditId { get; set; }

    public int ProductId { get; set; }

    public decimal? OldPrice { get; set; }

    public decimal? NewPrice { get; set; }

    public DateTime ChangedAt { get; set; }

    public string? ChangedBy { get; set; }
}
