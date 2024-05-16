using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    public class Money2DbModel
    {
        public int ID { get; set; }
        public string? Budget { get; set; }
    }
}