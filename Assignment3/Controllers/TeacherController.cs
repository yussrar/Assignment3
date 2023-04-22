using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //This is to render dynamic pages. Takes data from Teacher Data Controller and passes to list view
        //Get : /Teacher/List

        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> teachers = controller.ListTeachers(SearchKey);
            return View(teachers);
        }

        //This controller is to show one Teacher detail
        // /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //GET:/Teacher/New
        public ActionResult New()
        {
            return View();
        }


        //This controller is to create a teacher
        //POST:/Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherFname, string teacherLname, string TeacherEmployeeNo, int Salary, DateTime HireDate)
        {
            //identify this method is running
            Debug.WriteLine(teacherFname);
            Debug.WriteLine(teacherLname);
            Debug.WriteLine(TeacherEmployeeNo);
            Debug.WriteLine(Salary);
            Debug.WriteLine(HireDate);

            Teacher NewTeacher = new Teacher();
            NewTeacher.Salary = Salary;
            NewTeacher.TeacherLname = teacherLname;
            NewTeacher.TeacherFname = teacherFname;
            NewTeacher.TeacherEmployeeNo = TeacherEmployeeNo;
            NewTeacher.HireDate = HireDate;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        //This controller is to confirm to delete teacher
        //GET /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);

        }

        /// <summary>
        /// This controller is to delete teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return a webpage with list of the teacher</returns>
        /// <example> //POST: /Teacher/Delete/{id}</example>
        public ActionResult Delete(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// This controller is to update teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return a webpage where we can edit the information about selected teacher</returns>
        /// <example>POST: /Teacher/update/{id} </example>
        [HttpPost]
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController(); 
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the
        /// system, with new values. Conveys this information to the API, and redirects to 
        /// "show teacher" page of our updated teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherFname"></param>
        /// <param name="teacherLname"></param>
        /// <param name="TeacherEmployeeNo"></param>
        /// <param name="Salary"></param>
        /// <param name="HireDate"></param>
        /// <returns>A dynamic webpage which provides the current information of the teacher</returns>
        /// <example> POST: /Teacher/Update/{id} </example>
        public ActionResult Update(int id, string teacherFname, string teacherLname, string TeacherEmployeeNo, int Salary, DateTime HireDate)

        {
            Teacher TeacherInfo= new Teacher();

            Debug.WriteLine(teacherFname);
            Debug.WriteLine(teacherLname);
            Debug.WriteLine(TeacherEmployeeNo);
            Debug.WriteLine(Salary);
            Debug.WriteLine(HireDate);

            TeacherInfo.TeacherLname = teacherLname;
            TeacherInfo.TeacherFname = teacherFname;
            TeacherInfo.TeacherEmployeeNo = TeacherEmployeeNo;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}