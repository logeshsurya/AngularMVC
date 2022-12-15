import { Component,OnInit } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent {

   endpoint = "Employee";
   data:any;
   page:number=1;
   searchValue = '';
   public filteredData: any[]=[];
   id:any;


  constructor(private http : HttpClient,private employeeService:EmployeeService){}

 

  ngOnInit(): void {

    this.employeeService.GetAll(this.endpoint).subscribe((data)=>
      {
        this.data = data;
        this.filteredData = data;
        console.log(data);
      });

  }


  
  deleteEmployee(id:any)
  {
   
      this.employeeService.Delete(this.endpoint,id).subscribe((result) =>
      {
        console.log(result);
        this.ngOnInit();
        
      })
       
  
  }
  
  Search(value: string) {

    

    this.data = this.filteredData.filter(item =>

       
      item.firstname.toLowerCase().includes(value.toLowerCase()) || 
      item.lastname.toLowerCase().includes(value.toLowerCase()))

    console.log(this.data)

    this.page = 1;

  }
  // Onchange(){



  //   const item = document.getElementById('Category_Name') as HTMLSelectElement;
  
  //   this.productservice.getproductfilters(item.value).subscribe(res=>{
  
  //     console.warn(res);
  
  //     this.Product=res;
  
  //   })
  
  //   console.warn(item.value);
  
  
  
  // }
  
 
}
