using System.ComponentModel.DataAnnotations;

namespace MedicineMAnagementTool.Common.DTOs
{
    public class SaleDetailDTO
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }
        public int MedicineId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        public double TotalPrice { get; set; }
    }
}
