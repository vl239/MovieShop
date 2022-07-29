using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Models
{
    public class PurchasedMoviesModel
    {
        public Movie Movie { get; set; }
        public int PurchaseCount { get; set; }
    }
}

