using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    //public class QueryDto
    //{
    //    public int Year { get; set; }
    //}
    public class MoneyDbModel
    {
        [Key]
        public int ID { get; set; }

        public string? Budget { get; set; }

        public string? Group { get; set; }

        public string? Subject6 { get; set; }

        public string? Subject7 { get; set; }

        public string? Subject8 { get; set; }

        public int? BudgetYear { get; set; }

        public string? Final { get; set; }

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
        public List<Money3DbModel>? Money3DbModels { get; set; }
    }

}
