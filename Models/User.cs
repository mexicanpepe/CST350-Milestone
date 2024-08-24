using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CST350_Minesweeper.Models
{
    public class User
    {
        //User constructor with parameters
        public User(string firstname, string lastname, string sex, int age, string state, string email, string username, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            Sex = sex;
            Age = age;
            State = state;
            Email = email.ToLower();
            Username = username;
            Password = password;
        }

        //user constructor
        public User()
        {

        }

        public int UserID { get; set; }

        [Required]
        [DisplayName("User's First Name")]
        [StringLength(45)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("User's Last Name")]
        [StringLength(45)]
        public string LastName { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Users Gender")]
        public string Sex { get; set; }

        [Required]
        [DisplayName("User's Age")]
        [Range(5, 100, ErrorMessage = "Must be at least 5 years old to Register for Game!")]
        public int Age { get; set; }

        [Required]
        [StringLength(45)]
        public string State { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [StringLength(45)]
        public string Email { get; set; }

        [Required]
        [StringLength(45)]
        public string Username { get; set; }

        [Required]
        [StringLength(45, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters!")]
        public string Password { get; set; }
    }
}
