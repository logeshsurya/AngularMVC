using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public int Organisation { get; set; }
        public int Department { get; set; }
        public int Designation { get; set; }
        public string? Designation_Name { get; set; }
        public string? Department_Name { get; set; }
        public string? Organisation_Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

    }
}