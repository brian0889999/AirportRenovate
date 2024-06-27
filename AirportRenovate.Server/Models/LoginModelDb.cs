using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    public class LoginModelDb
    {
         [Key]
        public int No { get; set; }
        public string? Name { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Unit_No { get; set; }
        public string? Auth { get; set; }
        public string? Account_Open { get; set; }
        public string? Reason { get; set; }
        public int Count { get; set; }
        public DateTime Time { get; set; }
        public DateTime Time1 { get; set; }
        public string? Status1 { get; set; }
        public string? Status2 { get; set; }
        public string? Memo { get; set; }
        public string? Status3 { get; set; }
    }
}
