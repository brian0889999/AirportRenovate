using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
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
    }

    public class Money2DbModel
    {
        public int ID { get; set; }
        public string? Budget { get; set; }
    }

    public class Money3DbModel
    {
        [Key]
        public int ID { get; set; }

        public DateTime Purchasedate { get; set; }

        public string? Text { get; set; }

        public string? Note { get; set; }

        public int PurchaseMoney { get; set; }

        public DateTime PayDate { get; set; }

        public int PayMoney { get; set; }

        public string? People { get; set; }

        public string? Name { get; set; }

        public string? Remarks { get; set; }

        public string? People1 { get; set; }

        public int? ID1 { get; set; }

        public string? status { get; set; }

        public string? Group1 { get; set; }

        //[Column("[All]")]
        [Column("All")]
        public string? All { get; set; }

        public string? True { get; set; }

        public DateTime summonsdate { get; set; }

        public string? summonsnumber { get; set; }

        public int summonsmoney { get; set; }

        public string? summonsnote { get; set; }

        public DateTime accountdate { get; set; }

        public int year { get; set; }

        public string? year1 { get; set; }
    }
}
