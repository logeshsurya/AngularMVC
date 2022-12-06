using Microsoft.AspNetCore.Mvc;   
using User.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace User.Controllers
{  
    public class EmployeeController : Controller  
    {  
        
        EmployeeDAL employee_object = new EmployeeDAL(); 

        DepartmentDAL department_obj = new DepartmentDAL();

        DesignationDAL designation_obj = new DesignationDAL();

        OrganisationDAL organisation_obj = new OrganisationDAL();

        public void Dept_list()
        {
            
            ViewBag.departments = new SelectList(department_obj.GetDepartments(),"id","department_name");
        
        }
        public void Desg_list()
        {
            ViewBag.designations = new SelectList(designation_obj.GetDesignations(),"id","designation_name");
        }
        public void Org_list()
        {
            ViewBag.organisations = new SelectList(organisation_obj.GetOrganisations(),"id","Organisation_Name");
        }



        [HttpGet]

        [Route("GetAllEmployee")]
    
        public JsonResult Index()  
        {  

            var listEmployees = employee_object.GetAllEmployees().ToList();  
            return Json(listEmployees);  
        }  

      

  
[HttpPost]  
[Route("Employee/Create")] 
public JsonResult Create([FromBody] Employee employee)  
{  
    
        employee_object.AddEmployee(employee);  
        RedirectToAction("Index");
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
        // return RedirectToAction("Index");    
     
    return Json(employee);    
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
        return   NotFound();
    }  
    return Json(employee);  
} 


    //     [HttpGet]  
    // public IActionResult Delete(int? id)  
    // {  
    //     if (id == null)  
    //     {  
    //         return NotFound();  
    //     }  
    //     Employee employee = employee_object.GetEmployeeById(id);  
      
    //     if (employee == null)  
    //     {  
    //         return NotFound();  
    //     }  
    //     return View(employee);  
    // }  
      
    



     }  

}  