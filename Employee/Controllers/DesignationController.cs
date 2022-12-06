
using Microsoft.AspNetCore.Mvc;
using User.Models;

namespace User.Controllers
{
    public class DesignationController : Controller
    {

        DesignationDAL designation_object = new DesignationDAL();



        [HttpGet]
        [Route("GetAllDesignation")]

        public JsonResult Index()
        {

            var designation = designation_object.GetDesignations().ToList();

            return Json(designation);
        }
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create([Bind] Designation designation)
        {
            if (ModelState.IsValid)
            {
                designation_object.AddDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Designation designation = designation_object.GetDesignationById(id);

            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            designation_object.DeleteDesignation(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            Designation designation = designation_object.GetDesignationById(id);

            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Designation designation)
        {
            if (id != designation.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                designation_object.UpdateDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }

    }
}