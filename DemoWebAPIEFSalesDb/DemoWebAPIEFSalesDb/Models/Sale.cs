using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPIEFSalesDb.Models;

public partial class Sale
{
    [Key]
    public int Id { get; set; }

    public int SalesPersonId { get; set; }

    public DateOnly SalesDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? SalesAmount { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Sales")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("SalesPersonId")]
    [InverseProperty("Sales")]
    public virtual SalesPerson SalesPerson { get; set; } = null!;
}
