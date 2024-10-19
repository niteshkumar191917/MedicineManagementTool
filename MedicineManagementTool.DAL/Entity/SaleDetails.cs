using System.ComponentModel.DataAnnotations;

namespace MedicineManagementTool.DAL.Entity
{
    public class SaleDetails
    {
        [Key]
        public int SaleId { get; set; }

        
        public virtual User User { get; set; }
        public int UserId { get; set; }

        
        public virtual Medicine Medicine { get; set; }
        public int MedicineId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]        
        public double TotalPrice { get; set; }
    }
}