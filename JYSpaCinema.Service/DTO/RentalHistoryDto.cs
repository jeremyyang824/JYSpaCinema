using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYSpaCinema.Service.DTO
{
    public class RentalHistoryDto
    {
        public int ID { get; set; }
        public int StockId { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime RetalDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
