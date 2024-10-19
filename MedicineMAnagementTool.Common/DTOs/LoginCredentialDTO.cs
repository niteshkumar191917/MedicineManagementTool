using System.ComponentModel.DataAnnotations;

namespace MedicineMAnagementTool.Common.DTOs
{
    public class LoginCredentialDTO
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [StringLength(100, ErrorMessage = "Length should less than 100char")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
