using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Web.Mvc;  // Required for SelectListItem

namespace HR_Managemenr_System.Models
{
    public class Student
    {
        // Basic student information

        public int Id { get; set; }

        //[Required(ErrorMessage = "Student Name is required.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Contact Number is required.")]
        public string ContactNumber { get; set; }

        //[Required(ErrorMessage = "Class selection is required.")]
        public int ClassId { get; set; }

        //[Required(ErrorMessage = "At least one course must be selected.")]
       // public List<int> SelectedCourses { get; set; } = new List<int>();

      // public List<int> AvailableCourses { get; set; } = new List<int>();

        public int Class { get; set; } // Add ClassValue property
        
        public string Courses { get; set; } // Add a CourseValue Property

        public string CourseIds { get; set; } // New property for course IDs

        //public string MarkCourses { get; set; } //

        public string MarkedCourseIds { get; set; } // SelectedCourse Ids

        //public string AvailCourses { get; set; }

        public string AvailableCoursesIds { get; set; }


        //public string AllCourses { get; set; }


    }
}
