using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    public class Log
    {
        public string SQL { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
