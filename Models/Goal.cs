using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("Goal")]
public partial class Goal
{
    [Key]
    [Column("goalId")]
    public int GoalId { get; set; }

    [Column("userId")]
    public int UserId { get; set; }

    [Column("goalOutcome")]
    [StringLength(300)]
    [Unicode(false)]
    public string GoalOutcome { get; set; } = null!;

    [Column("completionDate")]
    public DateOnly CompletionDate { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("createdDate")]
    public DateOnly CreatedDate { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [Column("status")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Status { get; set; }

    [InverseProperty("Goal")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("GoalCreatedByNavigations")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("GoalUsers")]
    public virtual User User { get; set; } = null!;
}
