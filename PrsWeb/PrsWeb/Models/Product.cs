﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PrsWeb.Models;

[Table("Product")]
[Index("VendorId", "PartNumber", Name = "UQ_Product_vendor_part", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("vendorId")]
    public int VendorId { get; set; }

    [Column("partNumber")]
    [StringLength(50)]
    [Unicode(false)]
    public string PartNumber { get; set; } = null!;

    [Column("name")]
    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Column("unit")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Unit { get; set; }

    [Column("photoPath")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PhotoPath { get; set; }

    [JsonIgnore]
    [InverseProperty("Product")]
    public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();

    [ForeignKey("VendorId")]
    [InverseProperty("Products")]
    public virtual Vendor? Vendor { get; set; } = null!;
}
