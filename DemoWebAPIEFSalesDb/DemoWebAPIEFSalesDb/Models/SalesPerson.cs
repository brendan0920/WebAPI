using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPIEFSalesDb.Models;

[Table("SalesPerson")]
public partial class SalesPerson
{
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(25)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public int RegionId { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("SalesPeople")]
    public virtual Region Region { get; set; } = null!;

    [InverseProperty("SalesPerson")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
