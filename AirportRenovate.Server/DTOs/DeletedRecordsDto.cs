using System.ComponentModel.DataAnnotations;

namespace AirportRenovate.Server.DTOs
{
    public class DeletedRecordsDto
    {
        public int? ID { get; set; }
        public DateTime? Purchasedate { get; set; }
        public string? Text { get; set; }
        public string? Note { get; set; }
        public decimal? PurchaseMoney { get; set; }
        public DateTime? PayDate { get; set; }
        public decimal? PayMoney { get; set; }
        public string? People { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public string? People1 { get; set; }
        public int? ID1 { get; set; }
        public string? Status { get; set; }
        public string? Group1 { get; set; }
        public string? All { get; set; }
        public string? True { get; set; }
        public int? Year { get; set; }
        public string? Year1 { get; set; }
    }
}
