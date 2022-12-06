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

  organisations: any;
  departments: any;
  designations: any;
  result:any;
  
  Dob: any;
  error: any;
  DesignationName: any;
  constructor(private employeeService: EmployeeService, private router: ActivatedRoute, private routing: Router, private toastService: HotToastService,private http:HttpClient) { }

  ngOnInit(): void {
  

    this.getAllOrganisations().subscribe((data) => {
      this.organisations = data;
      console.log(data);
    });

    this.getAllDepartments().subscribe((data) => {
      this.departments = data;
      console.log(data);
    });

    this.getAllDesignations().subscribe((data) => {
      this.designations = data;
      console.log(data);
    });
    this.router.params.subscribe(params => {
      this.id = params['id'];
      this.employeeService.GetEmployeeById(this.endpoint, this.id).subscribe((result) => {
        this.update = result;
        console.log(this.update);
        this.selectedOrganisation = this.update.organisation;
        console.log(this.selectedOrganisation);
        this.selectedDepartment = this.update.department;
        this.selectedDesignation = this.update.designation;
      });

    });
   

  }

  
  getAllOrganisations(): Observable<any> {
    return this.http.get<any>('https://localhost:7256/GetAllOrganisation');
  }

  getAllDepartments(): Observable<any> {
    return this.http.get<any>('https://localhost:7256/GetAllDepartment');
  }

  getAllDesignations(): Observable<any> {
    return this.http.get<any>('https://localhost:7256/GetAllDesignation');
  }


  OnSubmit() {
    console.log(this.update);
    this.employeeService.Edit(this.endpoint, this.update).subscribe((data)=>{
     
     console.log(data);
    })
  }
  showToast() {
    this.toastService.success('Employee updated Successfully!',
      {
        autoClose: true,
        dismissible: true,
        icon: '❎',
      })
    // this.routing.navigate(['']);

  }

  changeDate() {
    document.getElementById("dob")?.setAttribute("max", this.change);
  }


}
