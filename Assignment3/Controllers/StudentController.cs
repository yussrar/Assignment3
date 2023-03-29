using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //This is to render dynamic pages. Takes data from Student Date Controller and passes to list view
        //Get : /Student/List

        public ActionResult List()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> students = controller.ListStudents();
            return View(students);
        }
    }
}