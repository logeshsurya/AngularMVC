using System.ComponentModel.DataAnnotations;
namespace Database.Models
{
    public class Employee
    {   
        public int id { get; set; }
        public int Department { get; set; }
        public int Designation { get; set; }
        public string? Designation_Name { get; set; }
        public string? Department_Name { get; set; }
        public string? Organisation_Name { get; set; }

        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Password { get; set; }
        [Required]
        public int Organisation { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime? DOB { get; set; }
    }

    public class Login
    {
        public string Email {get;set;}
        public string Password {get;set;}
    }

}
