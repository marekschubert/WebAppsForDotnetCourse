using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStudentWithIdentity.Data
{
    public enum Gender
    {
        Female, Male
    }
    public class Student
    {

        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "Write from 1 to 6 digits")]
        public int Index { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "To short name")]
        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {0}")]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime BirthDate { get; set; }

        public Student()
        {

        }

        public Student(int id, int index, string name, Gender gender, bool active, int departmentID, DateTime birthDate)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentID;
            this.BirthDate = birthDate;
        }
    }

}
