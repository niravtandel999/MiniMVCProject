using System.ComponentModel.DataAnnotations;

namespace MVCTaskProject.Models
{
    public class ForgotPassword
    {
        [Key]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Please enter a valid email address (e.g., xyz10@gmail.com).")]
        public string Email { get; set; }
    }
}
