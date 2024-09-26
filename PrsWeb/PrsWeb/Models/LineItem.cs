using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PrsWeb.Models;

[Table("LineItem")]
[Index("RequestId", "ProductId", Name = "UQ_LineItem_reqId_prodId", IsUnique = true)]
public partial class LineItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("requestId")]
    public int RequestId { get; set; }

    [Column("productId")]
    public int ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("LineItems")]
    public virtual Product? Product { get; set; } = null!;

    [ForeignKey("RequestId")]
    [InverseProperty("LineItems")]
    public virtual Request? Request { get; set; } = null!;
}
