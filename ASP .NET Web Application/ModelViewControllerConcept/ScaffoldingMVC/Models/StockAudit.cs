using System;
using System.Collections.Generic;

namespace ScaffoldingMVC.Models;

public partial class StockAudit
{
    public int AuditId { get; set; }

    public int ProductId { get; set; }

    public int? OldStock { get; set; }

    public int? NewStock { get; set; }

    public DateTime ChangedAt { get; set; }
}
