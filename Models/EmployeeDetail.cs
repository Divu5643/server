using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

public partial class EmployeeDetail
{
    [Key]
    [Column("detailId")]
    public int DetailId { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("dateofBirth")]
    public DateOnly? DateofBirth { get; set; }

    [Column("gender")]
    [StringLength(7)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("personalEmail")]
    [StringLength(300)]
    [Unicode(false)]
    public string? PersonalEmail { get; set; }

    [Column("phone")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("profileImage", TypeName = "text")]
    public string? ProfileImage { get; set; }

    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("EmployeeDetails")]
    public virtual User? User { get; set; }
}
