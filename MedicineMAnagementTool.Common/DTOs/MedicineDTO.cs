using System.ComponentModel.DataAnnotations;

namespace MedicineMAnagementTool.Common.DTOs
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Medicine code is required")]
        public string Code { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name only contain alphabets")]
        [MinLength(3, ErrorMessage = "Medicine length should be greater than 3 and less then 100")]
        [MaxLength(100, ErrorMessage = "Medicine length should be greater than 3 and less then 100")]
        public string Name { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount should be greater then 1")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
