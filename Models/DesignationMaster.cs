using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("DesignationMaster")]
public partial class DesignationMaster
{
    [Key]
    [Column("designationId")]
    public int DesignationId { get; set; }

    [Column("designation")]
    [StringLength(40)]
    [Unicode(false)]
    public string Designation { get; set; } = null!;

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("Designation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
