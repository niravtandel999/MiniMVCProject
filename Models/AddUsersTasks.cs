using System.ComponentModel.DataAnnotations;

namespace MVCTaskProject.Models
{
    public class AddUsersTasks
    {
        [Required(ErrorMessage = "Task Name is required.")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Task Description is required.")]
        [StringLength(100, ErrorMessage = "Task Description cannot be longer than 100 characters.")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "Create Date is required.")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Complete Date is required.")]
        [DataType(DataType.Date)]
        public DateTime CompleteDate { get; set; }

        [Required(ErrorMessage = "Task Status is required.")]
        public string TaskStatus { get; set; }
    }
}
