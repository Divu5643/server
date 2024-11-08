using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace perfomanceSystemServer.Models;

[Table("YearAndMonth")]
public partial class YearAndMonth
{
    [Key]
    [Column("yearId")]
    public int YearId { get; set; }

    [Column("fetchYear")]
    public DateOnly? FetchYear { get; set; }

    [Column("fetchMonth")]
    public DateOnly? FetchMonth { get; set; }
}
