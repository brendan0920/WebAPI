using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace bmdbWebAPIEF.Models;

[Table("Actor")]
[Index("FirstName", "LastName", "Birthdate", Name = "UQ_Actor_fn_ln_bd", IsUnique = true)]
public partial class Actor
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    [InverseProperty("Actor")]
    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();



}
