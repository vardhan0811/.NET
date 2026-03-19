using System;
using System.Collections.Generic;

namespace MVC1.Models;

public partial class Products
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQty { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
