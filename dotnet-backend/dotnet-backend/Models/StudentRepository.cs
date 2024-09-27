using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
using HR_Managemenr_System.Models;
using static System.Net.Mime.MediaTypeNames;
using HR_Managament_System.Models;

namespace HR_Managamenr_System.Models
{
    public class StudentRepository
    {
        //private static readonly string strConnectionString = "Data Source=DESKTOP-P7354T7\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True";

        private static readonly string strConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;Integrated Security=SSPI;";

        //private static readonly string strConnectionString = "Data Source=DESKTOP-P7354T7\\SQLEXPRESS;Initial Catalog=HRSystem;Integrated Security=True";

        // Method to get all courses from the database
        public static IEnumerable<SelectListItem> GetCourses()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var sql = "SELECT CourseId, CourseName FROM Course WHERE IsActive = 1";
                var courses = connection.Query(sql)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.CourseName
                    }).ToList();
                return courses;
            }
        }


        public static IEnumerable<SelectListItem> GetClasses()
        {
           
            using (var connection = new SqlConnection(strConnectionString))
            {
                var sql = "SELECT ClassId, ClassValue FROM Class";
                var classes = connection.Query<Classes>(sql).Select(c => new SelectListItem
                {
                    Value = c.ClassId.ToString(),        // Map ClassId to Value
                    Text = c.ClassValue.ToString()       // Map ClassValue to Text
                }).ToList();
                return classes;
            }
        }


        public static int InsertStudent(Student student,string courseIds)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var query = "InsertStudent"; // Stored procedure name

                var parameters = new
                {
                    Name = student.Name,
                    ContactNumber = student.ContactNumber,
                    //ClassId = student.ClassId,
                    ClassId = student.Class,
                    CourseIds = courseIds // Joining course IDs as comma-separated string
                };
                //connection.Execute(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return connection.QuerySingle<int>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        public static IEnumerable<Student> GetAllStudents()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                return connection.Query<Student>("GetAllStudents", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //public static Student GetStudentById(int studentId)
        //{
        //    using (var connection = new SqlConnection(strConnectionString))
        //    {
        //        var query = "GetStudentById"; // Stored procedure name
        //        var parameters = new { StudentId = studentId };

        //        // Execute the stored procedure and return the student object
        //        return connection.QuerySingleOrDefault<Student>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
        //    }
        //}
        public static Student GetStudentById(int studentId)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var query = "GetStudentById"; // Stored procedure name
                var parameters = new { StudentId = studentId };

                // Execute the stored procedure and return the result
                return connection.QuerySingleOrDefault<Student>(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        public static bool DeleteStudent(int studentId)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                // Update the parameter name to match the stored procedure's expectation
                var parameters = new { StudentId = studentId }; // Change StudentId to match the stored procedure parameter
                var result = connection.Execute("DeleteStudent", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0; // Return true if delete was successful
            }
        }

        public static bool UpdateStudent(int studentId, string name, string contactNumber, int classId, string courseIds)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var query = "UpdateStudent"; // Stored procedure name

                var parameters = new
                {
                    StudentId = studentId,
                    Name = name,
                    ContactNumber = contactNumber,
                    ClassId = classId,
                    MarkedCourseIds = courseIds // Joining course IDs as a comma-separated string
                };

                // Execute the stored procedure and get the number of affected rows
                var affectedRows = connection.Execute(query, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Return true if at least one row was updated
                return affectedRows > 0;
            }
        }



        //// Method to get all students from the database by calling a stored procedure
        //public IEnumerable<Student> GetAllStudents()
        //{
        //    using (var connection = new SqlConnection(strConnectionString))
        //    {
        //        var sql = "GetAllStudents"; // Stored procedure name
        //        var students = connection.Query<Student>(sql, commandType: System.Data.CommandType.StoredProcedure);
        //        return students;
        //    }
        //}




    }
}