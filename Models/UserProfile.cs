using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTaskProject.Models
{
    public class UserProfile
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
        public string ProfilePictureUrl { get; set; }


    }
}
