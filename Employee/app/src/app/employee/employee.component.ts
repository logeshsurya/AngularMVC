import { Component,OnInit } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent {

  endpoint = "Employee";
   data:any;
   page:number=1;
   searchValue = '';
   public filteredData: any[]=[];
   id:any;


  constructor(private http : HttpClient,private employeeService:EmployeeService){}

 

  ngOnInit(): void {

    this.getAllEmployees().subscribe((data)=>
      {
        this.data = data;
        this.filteredData = data;
        console.log(data);
      });

  }


  getAllEmployees():Observable<any> {

    return this.http.get<any>('https://localhost:7256/GetAllEmployee');

  }
  
  deleteEmployee(id:any)
  {
    this.employeeService.GetEmployeeById(this.endpoint,id).subscribe((data)=>
    {
      this.id = data.id;
      this.employeeService.Delete(this.endpoint,id).subscribe((result) =>
      {
        console.log(result);
        this.ngOnInit();
        
      })
      console.log(data);     
    })
  }
  
  Search(value: string) {

    this.data = this.filteredData.filter(item =>

      item.firstname.toLowerCase().includes(value.toLowerCase()))

    console.log(this.data)

    this.page = 1;

  }

  
 
}
