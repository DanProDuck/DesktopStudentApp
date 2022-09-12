using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesktopStudentApp.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Index { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
