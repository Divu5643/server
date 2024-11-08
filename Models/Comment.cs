using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("Comment")]
public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Column("goalId")]
    public int? GoalId { get; set; }

    public int? CreatedBy { get; set; }

    [Unicode(false)]
    public string? CommentText { get; set; }

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Comments")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("GoalId")]
    [InverseProperty("Comments")]
    public virtual Goal? Goal { get; set; }
}
