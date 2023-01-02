import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from 'src/app/employee.service';
import { HotToastService } from '@ngneat/hot-toast';
import { DatePipe, formatDate } from '@angular/common';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {
  today = new Date();
  pipe = new DatePipe('en-US');
  month = (this.today.getMonth());
  maxMonth = this.pipe.transform(this.month, 'MM');
  date = (this.today.getDate());
  maxDate = this.pipe.transform(this.date, 'dd');
  year = (this.today.getFullYear() - 18);
  change = (this.year + '-' + this.maxMonth + '-' + this.maxDate);
 
  selectedOrganisation:any;
  selectedDepartment:any;
  selectedDesignation:any;

  organisationID: any;
  departmentID: any;
  designationID: any;
  id: any = 0;
  update: any;


 
  endpoint = 'Employee';
  endpoint1= 'Organisation';
  endpoint2 = 'Department';
  endpoint3 = 'Designation' 

  organisations: any;
  departments: any;
  designations: any;
  result:any;
  
  Dob: any;
  error: any;
  DesignationName: any;
  constructor(private employeeService: EmployeeService, private router: ActivatedRoute, private routing: Router, private toastService: HotToastService,private http:HttpClient) { }

  ngOnInit(): void {
  
    this.router.params.subscribe(params => {
      this.id = params['id'];
      this.employeeService.GetById(this.endpoint, this.id).subscribe((result) => {
        this.update = result;
        console.log(this.update);
        this.selectedOrganisation = this.update.organisation;
        console.log(this.selectedOrganisation);
        this.selectedDepartment = this.update.department;
        this.selectedDesignation = this.update.designation;
      });

    });
   

 
    this.employeeService.GetAll(this.endpoint1).subscribe((data)=>
    {
      this.organisations = data;
      console.log(data);
    })
    
    this.employeeService.GetAll(this.endpoint2).subscribe((data)=>
    {
      this.departments = data;
      console.log(data);
    })

    this.employeeService.GetAll(this.endpoint3).subscribe((data)=>
    {
      this.designations = data;
      console.log(data);
    })

   

  }

  OnSubmit() {
    console.log(this.update);
    this.employeeService.Edit(this.endpoint, this.update).subscribe({
     
      next: (data) => {
        data ? this.showToast() : null;
      },
      error: (error) => (this.error = error.error.message),
    })
  }
  showToast() {
    this.toastService.success('Employee updated Successfully!',
      {
        autoClose: true,
        dismissible: true,
        icon: '❎',
      })
      // window.location.reload();
     this.routing.navigate(['']);
    

  }

  changeDate() {
    document.getElementById("dob")?.setAttribute("max", this.change);
  }


}
