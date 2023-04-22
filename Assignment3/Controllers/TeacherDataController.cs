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
using Mysqlx.Datatypes;

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
        /// returns teacher objects
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //creating a instance of connection
            MySqlConnection Conn= school.AccessDatabase();
            Conn.Open();

            //creating command query
            MySqlCommand cmd = Conn.CreateCommand();

            //query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower (@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname,' ',teacherlname)) like lower(@key) ";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
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


        /// <summary>
        /// it will return a teacher whose id is passed as parameter
        /// </summary>
        /// <example> Get api/TeacherData/FindTeacher/3 </example>
        /// <returns>
        /// returns teacher object
        /// </returns>
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

        
        /// <summary>
        /// it will add a new teacher in the database
        /// </summary>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create instance of connection
            MySqlConnection conn = school.AccessDatabase();

            //opening connection between server and database
            conn.Open();

            //creating command for database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query 
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, salary, hiredate) values (@TeacherFname, @TeacherLname, @TeacherEmployeeNo, @Salary, @HireDate)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherEmployeeNo", NewTeacher.TeacherEmployeeNo);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        /// <summary>
        /// it will delete the teacher from the database whose id is sent as parameter
        /// </summary>
        //POST: /api/TeacherData/DeleteTeacher/3
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create instance of connection
            MySqlConnection conn = school.AccessDatabase();

            //opening connection between server and database
            conn.Open();

            //creating command for database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query 
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create instance of connection
            MySqlConnection conn = school.AccessDatabase();

            //opening connection between server and database
            conn.Open();

            //creating command for database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query 
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@TeacherEmployeeNo, salary=@Salary, hiredate=@HireDate" ;
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherEmployeeNo", TeacherInfo.TeacherEmployeeNo);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();
        }


    }
}
