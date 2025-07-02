using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCTaskProject.Models
{
    public class UserProfileDto
    {
        [Key] // primary key
        public int ProfileId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string City { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Foreign key

        // for profilepicture
        public string ProfilePictureUrl { get; set; }
    }
}
