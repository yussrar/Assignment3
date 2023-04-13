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


        //This controller is to delete teacher
        //POST: /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

    }
}