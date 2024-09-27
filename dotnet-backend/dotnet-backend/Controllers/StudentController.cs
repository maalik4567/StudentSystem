using HR_Managamenr_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace dotnet_backend.Controllers
{
    [RoutePrefix("api")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors(origins: "https://localhost:44308", headers: "*", methods: "*")]
    [EnableCorsAttribute("*","*","*")]
    public class StudentController : ApiController
    {

        // GET: api/students
        [HttpGet]
        [Route("getAllStudents")]
        //[CustomAuthorize] // Apply authorization if needed
        public IHttpActionResult GetStudents()
        {
            var students = StudentRepository.GetAllStudents().ToList();
            return Ok(students); // Return the list of students with a 200 OK response
        }

        // GET: api/students/{id}
        [HttpGet]
        [Route("getStudent/{id:int}")]
        public IHttpActionResult GetStudentById(int id)
        {
            // Call the repository method to get the student by ID
            var student = StudentRepository.GetStudentById(id);

            // If the student is not found, return a 404 NotFound
            if (student == null)
            {
                return NotFound();
            }

            // Return the student data with a 200 OK status
            return Ok(student);
        }

        // POST: api/students
        //[HttpPost]
        //[Route("")]
        ////[CustomAuthorize]
        //public IHttpActionResult CreateStudent([FromBody] Student student, [FromBody] int[] selectedCourses)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Return validation errors
        //    }

        //    // Convert array of selected courses to a comma-separated string
        //    string courseIds = string.Join(",", selectedCourses);

        //    // Insert student into the database
        //    int studentId = StudentRepository.InsertStudent(student, courseIds);

        //    return CreatedAtRoute("GetStudent", new { id = studentId }, student); // Return 201 Created
        //}

        // POST: api/students
        //[HttpPost]
        //[Route("createStudent")]
        //public IHttpActionResult CreateStudent(Student student, int[] SelectedCourses)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Convert array of selected courses to comma-separated string
        //        string courseIds = string.Join(",", SelectedCourses);

        //        // Insert student into the database
        //        int studentId = StudentRepository.InsertStudent(student, courseIds);

        //        // Return the newly created student ID
        //        return Ok(studentId);
        //    }

        //    // If validation fails, return bad request with validation errors
        //    return BadRequest(ModelState);
        //}
        //[CustomAuthorize]
        //[HttpPost]
        //[Route("createStudent")]
        //public IHttpActionResult CreateStudent([FromBody] Student student, [FromBody] int[] selectedCourses)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Return validation errors
        //    }

        //    // Convert array of selected courses to comma-separated string
        //    string courseIds = string.Join(",", selectedCourses);

        //    // Insert student into the database
        //    int studentId = StudentRepository.InsertStudent(student, courseIds);

        //    return Ok(student); // Return 200 OK
        //}
        [HttpPost]
        [Route("createStudent")]
        public IHttpActionResult CreateStudent([FromBody] StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            // Extract the student and selected courses from the model
            var student = model.Student;
            var selectedCourses = model.MarkedCourses; // Assuming MarkedCourses contains selected course IDs

            // Convert array of selected courses to comma-separated string
            string courseIds = string.Join(",", selectedCourses);

            // Insert student into the database
            int studentId = StudentRepository.InsertStudent(student, courseIds);

            return Ok(student); // Return 200 OK
        }



        // PUT: api/students/{id}
        // POST api/student/edit/{id:int}
        [HttpPut]
        [Route("edit/{id:int}")]
        public IHttpActionResult EditStudent([FromBody] StudentViewModel viewModel)
        {
            if (viewModel == null || viewModel.Student == null)
            {
                return BadRequest("Invalid student data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check that the ID in the route matches the ID in the view model
            //if (viewModel.Student.Id != id)
            //{
              //  return BadRequest("Student ID mismatch.");
            //}

            // Joining the marked courses as a comma-separated string
            string courseIds = string.Join(",", viewModel.MarkedCourses);

            // Call the UpdateStudent method
            bool isUpdated = StudentRepository.UpdateStudent(
                viewModel.Student.Id,
                viewModel.Student.Name,
                viewModel.Student.ContactNumber,
                viewModel.Student.Class,
                courseIds
            );

            if (isUpdated)
            {
                return Ok("Student updated successfully.");
            }
            else
            {
                return NotFound(); // Student not found or not updated
            }
        }


        // DELETE: api/delete/{id}
        [HttpDelete]
        [Route("delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = StudentRepository.DeleteStudent(id);

                if (isDeleted)
                {
                    return Ok(new { message = "Student deleted successfully." });
                }
                else
                {
                    return NotFound(); // Student not found
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return 500 error in case of exception
            }
        }

        // GET: api/students/courses
        [HttpGet]
        [Route("courses")]
        //[CustomAuthorize]
        public IHttpActionResult GetCourses()
        {
            var courses = StudentRepository.GetCourses(); // Get available courses
            return Ok(courses); // Return courses with a 200 OK response
        }

        // GET: api/students/classes
        [HttpGet]
        [Route("classes")]
        //[CustomAuthorize]
        public IHttpActionResult GetClasses()
        {
            var classes = StudentRepository.GetClasses(); // Get available classes
            return Ok(classes); // Return classes with a 200 OK response
        }







    }
}
