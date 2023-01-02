using Microsoft.AspNetCore.Mvc;
using Database.Models;
using Database.DataAccessLayer;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Database.Controllers
{
    public class EmployeeController : Controller
    {
        string BaseUrl = "https://localhost:7031/";
        private readonly EmployeeDAL _employeeDAL;
        private readonly IConfiguration _configuration; 
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IConfiguration configuration,ILogger<EmployeeController> logger,EmployeeDAL employeeDAL)
        {
            _configuration = configuration;
             _logger = logger;
            _employeeDAL = employeeDAL;    
        }

        [HttpGet]
        [Route("Employee/GetAll")]
        public async Task<ActionResult> GetAll()
        {
            List<Employee> listEmp = new List<Employee>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ApiEmployee/GetAll");

                if(response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    listEmp = JsonConvert.DeserializeObject<List<Employee>>(data);
                }
            return Json(listEmp);
            }
        }

        [HttpGet]
        [Route("employee/getcount")]
        public JsonResult getall()
        {
            var countEmployees = _employeeDAL.GetEmployeeCount();
            return Json(countEmployees);
        }

        [HttpGet]
        [Route("Employee/GetEmployeeById")]
        public IActionResult Details(int? id)
        {
            Employee employee = _employeeDAL.GetEmployeeById(id);

            if (id == null)
            {
                return NotFound();
            }

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
            _employeeDAL.AddEmployee(employee);
            return Json(employee);
        }

        [HttpPost]
        [Route("Employee/Delete")]
        public JsonResult DeleteConfirmed(int? id)
        {
            _employeeDAL.DeleteEmployee(id);
            return Json(id);
        }

        [HttpPut]
        [Route("Employee/Update")]
        public IActionResult Edit([FromBody] Employee employee)
        {
            _employeeDAL.UpdateEmployee(employee);
            return Json(employee);
        }
    }
}



 //  [HttpGet]
        // [Route("Employee/Filters")]
        // public JsonResult Productfilter(int? id)  
        // {  
        //     var listEmployees = employee_object.GetEmployeeFilter(id).ToList();  
        //     return Json(listEmployees);  
        // }
    //  }





// employees.component.ts:50 ERROR 
// HttpErrorResponse {headers: HttpHeaders, status: 0, statusText: 'Unknown Error',
//  url: 'http://localhost:7256/Employee/GetAll?pageNo=1&size=5&sort=firstname', ok: false, …}
// error
// : 
// ProgressEvent {isTrusted: true, lengthComputable: false, loaded: 0, total: 0, type: 'error', …}
// headers
// : 
// HttpHeaders {normalizedNames: Map(0), lazyUpdate: null, headers: Map(0)}
// message
// : 
// "Http failure response for http://localhost:7256/Employee/GetAll?pageNo=1&size=5&sort=firstname: 0 Unknown Error"
// name
// : 
// "HttpErrorResponse"
// ok
// : 
// false
// status
// : 
// 0
// statusText
// : 
// "Unknown Error"
// url
// : 
// "http://localhost:7256/Employee/GetAll?pageNo=1&size=5&sort=firstname"