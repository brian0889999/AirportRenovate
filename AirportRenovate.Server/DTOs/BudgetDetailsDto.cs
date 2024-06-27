﻿using AirportRenovate.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportRenovate.Server.DTOs;

public class BudgetDetailsDto
{
    public string Budget { get; set; } = string.Empty;
    public string Subject6 { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Subject7 { get; set; } = string.Empty;
    public string Subject8 { get; set; } = string.Empty;
    public int? BudgetYear { get; set; }
    public decimal? Final { get; set; } 
    public decimal? General { get; set; }
    public decimal? Out { get; set; }
    public decimal? UseBudget { get; set; }
    [Column("In")]
    public decimal? In { get; set; }
    public decimal? InActual { get; set; }
    public decimal? InBalance { get; set; }
    public decimal? SubjectActual { get; set; }
}
