using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("DepartmentMaster")]
public partial class DepartmentMaster
{
    [Key]
    [Column("deptId")]
    public int DeptId { get; set; }

    [Column("department")]
    [StringLength(40)]
    [Unicode(false)]
    public string Department { get; set; } = null!;

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [InverseProperty("Dept")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
