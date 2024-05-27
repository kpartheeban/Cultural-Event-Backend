using System.ComponentModel.DataAnnotations;

namespace CulturalEvent.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Student ID is required.")]
        public string StudentId { get; set; }

        //[Required(ErrorMessage = "Student name is required.")]
        //[StringLength(50, ErrorMessage = "Student name cannot be longer than 50 characters.")]
        public string StudentName { get; set; }

        //[Required(ErrorMessage = "Student email is required.")]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        public string StudentEmail { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm password is required.")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Phone number is required.")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Department is required.")]
        public string Dept { get; set; }

        //[Required(ErrorMessage = "Year is required.")]
        public string Year { get; set; }

    }
}
