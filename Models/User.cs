using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Index("Email", Name = "UQ__userMast__AB6E6164542EC10F", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("name")]
    [StringLength(70)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [Column("password")]
    [StringLength(300)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("roleId")]
    public int RoleId { get; set; }

    [Column("deptId")]
    public int DeptId { get; set; }

    [Column("designationId")]
    public int DesignationId { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("DeptId")]
    [InverseProperty("Users")]
    public virtual DepartmentMaster Dept { get; set; } = null!;

    [ForeignKey("DesignationId")]
    [InverseProperty("Users")]
    public virtual DesignationMaster Designation { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<EmployeeDetail> EmployeeDetails { get; set; } = new List<EmployeeDetail>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Goal> GoalCreatedByNavigations { get; set; } = new List<Goal>();

    [InverseProperty("User")]
    public virtual ICollection<Goal> GoalUsers { get; set; } = new List<Goal>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Performance> PerformanceCreatedByNavigations { get; set; } = new List<Performance>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Performance> PerformanceModifiedByNavigations { get; set; } = new List<Performance>();

    [InverseProperty("User")]
    public virtual ICollection<Performance> PerformanceUsers { get; set; } = new List<Performance>();

    [InverseProperty("Employee")]
    public virtual ICollection<Reviewer> ReviewerEmployees { get; set; } = new List<Reviewer>();

    [InverseProperty("Manager")]
    public virtual ICollection<Reviewer> ReviewerManagers { get; set; } = new List<Reviewer>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual RoleMaster Role { get; set; } = null!;
}
