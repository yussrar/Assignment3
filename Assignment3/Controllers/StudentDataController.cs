using Assignment3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        
        //database class to access our my sql school Database
        private SchoolDbContext school = new SchoolDbContext();

        //this controller will take the teachers from school DB
        /// <summary>
        /// it will return a list of students
        /// </summary>
        /// <example> Get api/StudentData/ListStudent </example>
        /// <returns>
        /// A list of student with first and last name
        /// </returns>

        [HttpGet]

        public IEnumerable<Student> ListStudents()
        {
            //creating a instance of connection
            MySqlConnection Conn = school.AccessDatabase();
            Conn.Open();

            //creating command query
            MySqlCommand cmd = Conn.CreateCommand();

            //query
            cmd.CommandText = "Select * from students";

            // storing the result of query in a variable 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creating an empty string for teachers
            List<Student> Students = new List<Student> { };

            //loop through each row of the result 
            while (ResultSet.Read())
            {
                //Access column information by DB column name 
                
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentLname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                //int StudentID = (int)ResultSet["studentid"];


                Student newStudent = new Student();

                //newStudent.StudentId = StudentID;
                newStudent.StudentFname= StudentFname;
                newStudent.StudentLname = StudentLname;
                newStudent.StudentNumber = StudentNumber;
                newStudent.EnrolDate = EnrolDate;

                //adding this name to the empty list we created earlier
                Students.Add(newStudent);
            }
            //closing connection
            Conn.Close();

            //returning the list of Teacher names
            return Students;
        }
    }
}
