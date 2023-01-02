using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers
{
    public class DesignationController : Controller
    {
        private readonly DesignationDAL designation_object ;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public DesignationController(IConfiguration configuration,ILogger<DesignationController> logger)
        {
            _configuration = configuration;
            _logger = logger;
             designation_object = new DesignationDAL(_configuration.GetConnectionString("Default"));

        }

        [HttpGet]
        [Route("Designation/GetAll")]
        public JsonResult Index()
        {
            var listDesignations = designation_object.GetAllDesignations().ToList();
            _logger.LogInformation("Get Designations");
            return Json(listDesignations);
        }

        [HttpGet]
        [Route("Designation/GetById")]
        public IActionResult Details(int? id)
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
            return Json(designation);
        }
        
        [HttpPost]
        [Route("Designation/Create")]    
        public JsonResult Create([FromBody] Designation designation)
        {
            if (ModelState.IsValid)
            {
                designation_object.AddDesignation(designation);
            }
            return Json(designation);
        }

        [HttpPost]
        [Route("Designation/Delete")]
        public JsonResult DeleteConfirmed(int? id)
        {
            designation_object.DeleteDesignation(id);
            return Json(id);
        }

        [HttpPut]
        [Route("Designation/Update")]
        public JsonResult Edit([FromBody] Designation designation)
        {
            if (ModelState.IsValid)
            {
                designation_object.UpdateDesignation(designation);  
            }
            return Json(designation);
        }
    }
}