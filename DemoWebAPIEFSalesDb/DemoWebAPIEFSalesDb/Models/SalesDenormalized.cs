using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPIEFSalesDb.Models;

[Table("Sales_Denormalized")]
public partial class SalesDenormalized
{
    [Key]
    public int Id { get; set; }

    public int SalesPersonId { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string SalesPersonFirstName { get; set; } = null!;

    [StringLength(25)]
    [Unicode(false)]
    public string SalesPersonLastName { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Region { get; set; } = null!;

    public DateOnly SalesDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? SalesAmount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Customer { get; set; }
}
