using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    public class User
    {
         [Key]
        public int No { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Account { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Unit_No { get; set; } = string.Empty;
        public string? Auth { get; set; } = string.Empty;
        public string? Account_Open { get; set; } = string.Empty;
        public string? Reason { get; set; } = string.Empty;
        public int Count { get; set; } 
        public DateTime Time { get; set; }
        public DateTime Time1 { get; set; }
        public string? Status1 { get; set; } = string.Empty;
        public string? Status2 { get; set; } = string.Empty;
        public string? Memo { get; set; } = string.Empty;
        public string? Status3 { get; set; } = string.Empty;
    }
}
