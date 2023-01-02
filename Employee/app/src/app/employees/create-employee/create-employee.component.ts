import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { EmployeeService } from 'src/app/employee.service';
import { HotToastService } from '@ngneat/hot-toast';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css'],
})
export class CreateEmployeeComponent implements OnInit {
  today = new Date();
  pipe = new DatePipe('en-US');
  month = this.today.getMonth();
  maxMonth = this.pipe.transform(this.month, 'MM');
  date = this.today.getDate();
  maxDate = this.pipe.transform(this.date, 'dd');
  year = this.today.getFullYear() - 18;
  change = this.year + '-' + this.maxMonth + '-' + this.maxDate;
  organisationID: any;
  departmentID: any;
  designationID: any;

  constructor(
    private http: HttpClient,
    private router: Router,
    private employeeService: EmployeeService,
    private toastService: HotToastService
  ) {}

  error: any = '';

  endpoint = 'Employee';
  endpoint1 = 'Organisation';
  endpoint2 = 'Department';
  endpoint3 = 'Designation';

  Employee: any = {
    id: 0,
    firstname: '',
    lastname: '',
    email: '',
    dob: Date,
    gender: '',
    Organisation: 0,
    Department: 0,
    Designation: 0,
  };

  organisations: any;
  departments: any;
  designations: any;

  SelectOrganisation: any = 0;
  SelectDepartment: any = 0;
  SelectDesignation: any = 0;

  ngOnInit(): void {
    this.employeeService.GetAll(this.endpoint1).subscribe((data)=>
    {
      this.organisations = data;
      console.log(data);
    })

    this.employeeService.GetAll(this.endpoint2).subscribe((data) => {
      this.departments = data;
      console.log(data);
    });

    this.employeeService.GetAll(this.endpoint3).subscribe((data) => {
      this.designations = data;
      console.log(data);
    });
  }

  

  OnSubmit() {
    console.log(this.Employee);
    this.employeeService.Add(this.endpoint, this.Employee).subscribe({
      next: (data) => {
        data ? this.showToast() : null;
      },
      error: (error) => (this.error = error.error.message),
    });
    
  }

  showToast() {
    this.toastService.success('Employee Added Succesfully!', {
      autoClose: true,
      dismissible: true,
      icon: '❎',
    });
    this.router.navigate(['']);
    // window.location.reload();
  }

  changeDate() {
    document.getElementById('dob')?.setAttribute('max', this.change);
  }
}
