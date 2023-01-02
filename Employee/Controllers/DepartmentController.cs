using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentDAL department_object;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public DepartmentController(IConfiguration configuration,ILogger<DepartmentController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            department_object = new DepartmentDAL(_configuration.GetConnectionString("Default"));
        }

        [HttpGet]
        [Route("Department/GetAll")]
        public JsonResult Index()
        {
            var department = department_object.GetAllDepartments().ToList();

            return Json(department);
        }

        [HttpGet]
        [Route("Department/GetById")]
        public JsonResult Details(int? id)
        {   
            Department department = department_object.GetDepartmentById(id);
            return Json(department);
        }

        [HttpPost]
        [Route("Department/Create")]
        public JsonResult Create([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                department_object.AddDepartment(department);
            }
            return Json(department);
        }

        [HttpPost]
        [Route("Department/Delete")]
        public JsonResult DeleteConfirmed(int? id)
        {
            department_object.DeleteDepartment(id);
            return Json(id);
        }

        [HttpPut]
        [Route("Department/Update")]
        public JsonResult Edit([FromBody] Department department)
        {   
            if (ModelState.IsValid)
            {
                department_object.UpdateDepartment(department);
            }
            return Json(department);
        }
    }
}