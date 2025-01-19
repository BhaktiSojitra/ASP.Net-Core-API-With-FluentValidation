using System.ComponentModel.DataAnnotations;
namespace API_DEMO.Models
{
    public class BillsModel
    {
        public int? BillID { get; set; }
        public string BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal NetAmount { get; set; }
        public int UserID { get; set; }
    }
}
