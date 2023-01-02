using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers
{
    public class OrganisationController : Controller
    {
        private readonly OrganisationDAL organisation_object;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrganisationController> _logger;
        public OrganisationController(IConfiguration configuration, ILogger<OrganisationController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            organisation_object = new OrganisationDAL(_configuration.GetConnectionString("Default"));
        }

        [HttpGet]
        [Route("Organisation/GetAll")]
        public JsonResult Index()
        {
            var listOrganisations = organisation_object.GetAllOrganisations().ToList();
            _logger.LogInformation("Get organisation");
            return Json(listOrganisations);
        }

        [HttpGet]
        [Route("Organisation/GetById")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Organisation organisation = organisation_object.GetOrganisationById(id);
            return Json(organisation);
        }

        [HttpPost]
        [Route("Organisation/Create")]
        public JsonResult Create([FromBody] Organisation organisation)
        {
            organisation_object.AddOrganisation(organisation);
            return Json(organisation);
        }

        [HttpPost]
        [Route("Organisation/Delete")]
        public JsonResult DeleteConfirmed(int? id)
        {
            organisation_object.DeleteOrganisation(id);
            return Json(id);
        }

        [HttpPut]
        [Route("Organisation/Update")]
        public IActionResult Edit([FromBody] Organisation organisation)
        {   
            organisation_object.UpdateOrganisation(organisation);   
            return Json(organisation);
        }
    }
}