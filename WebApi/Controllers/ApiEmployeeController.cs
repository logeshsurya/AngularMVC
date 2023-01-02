using Database.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ApiEmployeeController : ControllerBase
    {
         private readonly EmployeeDAL employee_object;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiEmployeeController> _logger;

        public ApiEmployeeController(ILogger<ApiEmployeeController> logger, IConfiguration configuration,EmployeeDAL _employeeobj)
        {
            _logger = logger;
            _configuration = configuration;
            employee_object = _employeeobj;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllEmployee()
        {
            var listEmployees = employee_object.GetAllEmployees().ToList();
            return Ok(listEmployees);
        }
    }
}