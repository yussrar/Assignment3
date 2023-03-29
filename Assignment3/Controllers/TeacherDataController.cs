using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;

namespace Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        //database class to access our my sql school Database
        private SchoolDbContext school = new SchoolDbContext();

        //this controller will take the teachers from school DB
        /// <summary>
        /// it will return a list of teachers
        /// </summary>
        /// <example> Get api/TeacherData/ListTeachers </example>
        /// <returns>
        /// A list of teachers with first and last name
        /// </returns>

        [HttpGet]

        public IEnumerable<Teacher> ListTeachers()
        {
            //creating a instance of connection
            MySqlConnection Conn= school.AccessDatabase();
            Conn.Open();

            //creating command query
            MySqlCommand cmd = Conn.CreateCommand();

            //query
            cmd.CommandText = "Select * from teachers";

           // storing the result of query in a variable 
           MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creating an empty string for teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //loop through each row of the result 
            while(ResultSet.Read())
            {
                //Access column information by DB column name 
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];
                string TeacherEmployeeNo = (string)ResultSet["employeenumber"];
                decimal Salary = (decimal)ResultSet["salary"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                

                Teacher newTeacher = new Teacher();

                newTeacher.TeacherId = TeacherID;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname= TeacherLname;
                newTeacher.TeacherEmployeeNo= TeacherEmployeeNo;
                newTeacher.Salary = Salary;
                newTeacher.HireDate = HireDate;

                //adding this name to the empty list we created earlier
                Teachers.Add(newTeacher);
            }
            //closing connection
            Conn.Close();

            //returning the list of Teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher newTeacher = new Teacher();


            //creating a instance of connection
            MySqlConnection Conn = school.AccessDatabase();
            Conn.Open();

            //creating command query
            MySqlCommand cmd = Conn.CreateCommand();

            //query
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            // storing the result of query in a variable 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access column information by DB column name 
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];
                string TeacherEmployeeNo = (string)ResultSet["employeenumber"];
                decimal Salary = (decimal)ResultSet["salary"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                

                newTeacher.TeacherId = TeacherID;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.TeacherEmployeeNo = TeacherEmployeeNo;
                newTeacher.Salary = Salary;
                newTeacher.HireDate = HireDate;

            }


                return newTeacher;

        }

    }
}
