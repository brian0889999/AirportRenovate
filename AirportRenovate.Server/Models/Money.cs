using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AirportRenovate.Server.Models
{
    //public class QueryDto
    //{
    //    public int Year { get; set; }
    //}
    public class Money
    {
        [Key]
        public int ID { get; set; }

        public string? Budget { get; set; } = string.Empty;

        public string? Group { get; set; } = string.Empty;

        public string? Subject6 { get; set; } = string.Empty;

        public string? Subject7 { get; set; } = string.Empty;

        public string? Subject8 { get; set; } = string.Empty;

        public int? BudgetYear { get; set; }

        public string? Final { get; set; } = string.Empty;

        public int? General { get; set; }

        public int? Out { get; set; }

        public int? UseBudget { get; set; }

        [Column("In")]
        public int? In { get; set; }

        public int? InActual { get; set; }

        public int? InBalance { get; set; }

        public int? SubjectActual { get; set; }

        public int? Year { get; set; }

        //[ForeignKey("Budget")]
        [JsonIgnore]
        public ICollection<Money3>? Money3s { get; set; }
        //public List<Money3DbModel>? Money3DbModels { get; set; }
    }

}
