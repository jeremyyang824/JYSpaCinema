using System;
using System.Collections.Generic;

namespace JYSpaCinema.Service.DTO
{
    public class TotalRentalHistoryDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int TotalRentals { get { return Rentals.Count; } }

        public List<RentalHistoryPerDate> Rentals { get; set; } = new List<RentalHistoryPerDate>();
    }

    public class RentalHistoryPerDate
    {
        public int TotalRentals { get; set; }
        public DateTime Date { get; set; }
    }
}
