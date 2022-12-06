
using Microsoft.AspNetCore.Mvc;
using User.Models;

namespace User.Controllers
{
    public class DepartmentController : Controller
    {

        DepartmentDAL department_object = new DepartmentDAL();


        [HttpGet]
        [Route("GetAllDepartment")]

        public JsonResult Index()
        {
            var department = department_object.GetDepartments().ToList();

            return Json(department);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create([Bind] Department department)
        {
            if (ModelState.IsValid)
            {
                department_object.AddDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = department_object.GetDepartmentById(id);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            department_object.DeleteDepartment(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            Department department = department_object.GetDepartmentById(id);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Department department)
        {
            if (id != department.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                department_object.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }


    }
}