using System.ComponentModel.DataAnnotations;

namespace MedicineMAnagementTool.Common.DTOs
{
    public class UserDTO
    {
        [Required]
        [StringLength(30, ErrorMessage = "Length should less than 30char")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name only contain alphabets")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [StringLength(100, ErrorMessage = "Length should less than 100char")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{6,20}$", ErrorMessage = "Password should contain one upper case, one lower case, one special symbol and one digit and length should be in between 6-20")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]        
        public string ConfirmPassword { get; set; }
    }
}
