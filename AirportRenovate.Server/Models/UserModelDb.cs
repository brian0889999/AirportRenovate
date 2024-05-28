using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    public class UserModelDb
    {
        [Key]
        public int No { get; set; }
        public string? Name { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Auth { get; set; }
        public string? Status1 { get; set; }
        public string? Status2 { get; set; }
        public string? Status3 { get; set; }
    }
}
