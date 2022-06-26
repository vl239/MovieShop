using System;
namespace ApplicationCore.Models
{
	public class PurchaseRequestModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
    }
}

