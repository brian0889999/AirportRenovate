namespace AirportRenovate.Server.ViewModels
{
    public class MoneyDbModel
    {
        public int? ID { get; set; }
        public string? Budget { get; set; }
        public string? Group { get; set; }
        public string? Subject6 { get; set; }
        public string? Subject7 { get; set; }
        public string? Subject8 { get; set; }
        public decimal? BudgetYear { get; set; }
        public string? Final { get; set; }
        public int? Year { get; set; }
    }

    public class SoftDeleteViewModel
    {
        public int? ID { get; set; }
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
        public int ID1 { get; set; }
        public string? Status { get; set; }
        public string? Group1 { get; set; }
        public string? All { get; set; }
        public string? True { get; set; }
        public int? Year { get; set; }
        public string? Year1 { get; set; }
        public MoneyDbModel? MoneyDbModel { get; set; }
        public string? FormattedPurchaseDate { get; set; }
        public string? FormattedPayDate { get; set; }
    }
}
