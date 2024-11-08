using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("RoleMaster")]
public partial class RoleMaster
{
    [Key]
    [Column("roleId")]
    public int RoleId { get; set; }

    [Column("role")]
    [StringLength(30)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
