using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PrsWeb.Models;

[Table("User")]
[Index("Email", Name = "UQ_User_email", IsUnique = true)]
[Index("FirstName", "LastName", "PhoneNumber", Name = "UQ_User_fn_ln_phone", IsUnique = true)]
[Index("Username", Name = "UQ_User_username", IsUnique = true)]
public partial class User 
{  
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(20)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(10)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("firstName")]
    [StringLength(20)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("lastName")]
    [StringLength(20)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("phoneNumber")]
    [StringLength(12)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("email")]
    [StringLength(75)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("reviewer")]
    public bool Reviewer { get; set; }

    [Column("admin")]
    public bool Admin { get; set; }

    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
