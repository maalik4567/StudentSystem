using HR_Managemenr_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Managamenr_System.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<SelectListItem> Classes { get; set; }
        public List<SelectListItem> Courses { get; set; }
        public IEnumerable<Student> Students { get; set; }

        public int[] MarkedCourses { get; set; }

        public int[] AvailableCourses { get; set; }






    }
}