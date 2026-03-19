using System;
using System.Collections.Generic;

namespace ScaffoldingMVC.Models;

public partial class ReorderLog
{
    public int LogId { get; set; }

    public int ProductId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
