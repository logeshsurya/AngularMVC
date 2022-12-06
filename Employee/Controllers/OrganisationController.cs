
using Microsoft.AspNetCore.Mvc;
using User.Models;

namespace User.Controllers
{
    public class OrganisationController : Controller
    {
        OrganisationDAL organisation_object = new OrganisationDAL();



        [HttpGet]
        [Route("GetAllOrganisation")]
        public JsonResult Index()
        {
            var organisation = organisation_object.GetOrganisations().ToList();
            return Json(organisation);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create([Bind] Organisation organisation)
        {
            if (ModelState.IsValid)
            {
                organisation_object.AddOrganisation(organisation);
                return RedirectToAction("Index");
            }
            return View(organisation);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Organisation organisation = organisation_object.GetOrganisationById(id);

            if (organisation == null)
            {
                return NotFound();
            }
            return View(organisation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            organisation_object.DeleteOrganisation(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            Organisation organisation = organisation_object.GetOrganisationById(id);

            if (organisation == null)
            {
                return NotFound();
            }
            return View(organisation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Organisation organisation)
        {
            if (id != organisation.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                organisation_object.UpdateOrganisation(organisation);
                return RedirectToAction("Index");
            }
            return View(organisation);
        }

    }
}