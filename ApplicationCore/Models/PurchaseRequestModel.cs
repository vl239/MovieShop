using System;
namespace ApplicationCore.Models
{
	public class PurchaseRequestModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
    }
}

