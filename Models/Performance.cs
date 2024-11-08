using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("Performance")]
public partial class Performance
{
    [Key]
    [Column("performanceId")]
    public int PerformanceId { get; set; }

    [Column("userId")]
    public int UserId { get; set; }

    [Column("technicalSkill")]
    public int TechnicalSkill { get; set; }

    [Column("softSkill")]
    public int SoftSkill { get; set; }

    [Column("teamwork")]
    public int Teamwork { get; set; }

    [Column("deliveryTime")]
    public int DeliveryTime { get; set; }

    [Column("remark")]
    [StringLength(1500)]
    [Unicode(false)]
    public string? Remark { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [Column("createdDate")]
    public DateOnly CreatedDate { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("modifiedDate")]
    public DateOnly? ModifiedDate { get; set; }

    [Column("modifiedBy")]
    public int? ModifiedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("PerformanceCreatedByNavigations")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ModifiedBy")]
    [InverseProperty("PerformanceModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("PerformanceUsers")]
    public virtual User User { get; set; } = null!;
}
