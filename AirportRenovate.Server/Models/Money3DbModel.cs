using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.Models
{
    public class Money3DbModel
    {
        [Key]
        public int ID { get; set; }

        public DateTime? Purchasedate { get; set; }

        public string? Text { get; set; }

        public string? Note { get; set; }

        public int? PurchaseMoney { get; set; }

        public DateTime? PayDate { get; set; }

        public int? PayMoney { get; set; }

        public string? People { get; set; }

        public string? Name { get; set; }

        public string? Remarks { get; set; }

        public string? People1 { get; set; }

        public int? ID1 { get; set; }

        public string? Status { get; set; }

        public string? Group1 { get; set; }

        //[Column("[All]")]
        [Column("All")]
        public string? All { get; set; }

        public string? True { get; set; }

        public DateTime? Summonsdate { get; set; }

        public string? Summonsnumber { get; set; }

        public int? Summonsmoney { get; set; }

        public string? Summonsnote { get; set; }

        public DateTime? Accountdate { get; set; }

        public int? Year { get; set; }

        public string? Year1 { get; set; }

        //[ForeignKey("Name")]
        public MoneyDbModel? MoneyDbModel { get; set; }
    }
}
