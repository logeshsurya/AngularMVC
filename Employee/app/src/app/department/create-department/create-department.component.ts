import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from 'src/app/employee.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styleUrls: ['./create-department.component.css']
})
export class CreateDepartmentComponent implements OnInit {
 
  constructor(private toastService: HotToastService, private employeeService:EmployeeService, private router: Router) { }

  data: any;
  error: any;
  endpoint = "Department"

  Department:any = {
    id:0,
    department_Name:''
  }
  

  ngOnInit(): void {

 
  }

  OnSubmit() {
    this.employeeService.Add(this.endpoint,this.Department).subscribe(
      {
        next:(data)=> {
          data ? this.showToast():null;
        },
        error:(error) => (this.error=error.error.message),
      }
    );
  }
  showToast() {
    this.toastService.success('Department added Successfully !',
      {
        autoClose: true,
        dismissible: true,

      })
    this.router.navigate(['/department']);
  }

}







