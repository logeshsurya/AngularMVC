using Microsoft.AspNetCore.Mvc;
using Database.Models;


namespace Database.Controllers
{
    public class EmployeeController : Controller
    {


        private readonly EmployeeDAL employee_object;

        private readonly IConfiguration _configuration;
        
         private readonly ILogger<EmployeeController> _logger;


        public EmployeeController(IConfiguration configuration,ILogger<EmployeeController> logger)
        {
            _configuration = configuration;
             _logger = logger;
             employee_object = new EmployeeDAL(_configuration.GetConnectionString("Default"));
        }



        

        [HttpGet]

        [Route("Employee/GetAll")]

        public JsonResult Index()
        {

            var listEmployees = employee_object.GetAllEmployees().ToList();
            _logger.LogInformation("Get all employees");
            return Json(listEmployees);
        }

        [HttpGet]

        [Route("Employee/GetEmployeeById")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = employee_object.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return Json(employee);
        }

        [HttpPost]

        [Route("Employee/Create")]
        public JsonResult Create([FromBody] Employee employee)
        {

            employee_object.AddEmployee(employee);
            return Json(employee);
        }

        [HttpPost]
        [Route("Employee/Delete")]
        public JsonResult DeleteConfirmed(int? id)
        {
            employee_object.DeleteEmployee(id);
            return Json(id);
        }


        [HttpPut]
        [Route("Employee/Update")]
        public IActionResult Edit([FromBody] Employee employee)
        {


            employee_object.UpdateEmployee(employee);
              

            return Json(employee);
        }


        //  [HttpGet]

        // [Route("Employee/Filters")]

        // public JsonResult Productfilter(int? id)  

        // {  

        //     var listEmployees = employee_object.GetEmployeeFilter(id).ToList();  

        //     return Json(listEmployees);  

        // }

    //  }

     
    }

}