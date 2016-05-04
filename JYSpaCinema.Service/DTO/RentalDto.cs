using System;

namespace JYSpaCinema.Service.DTO
{
    public class RentalDto
    {
        public int ID { get; set; }
        public int StockId { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime RetalDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
