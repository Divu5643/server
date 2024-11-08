using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("Reviewer")]
public partial class Reviewer
{
    [Key]
    [Column("reviewerId")]
    public int ReviewerId { get; set; }

    [Column("employeeId")]
    public int EmployeeId { get; set; }

    [Column("managerId")]
    public int ManagerId { get; set; }

    [Column("reviewType")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ReviewType { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("ReviewerEmployees")]
    public virtual User Employee { get; set; } = null!;

    [ForeignKey("ManagerId")]
    [InverseProperty("ReviewerManagers")]
    public virtual User Manager { get; set; } = null!;
}
