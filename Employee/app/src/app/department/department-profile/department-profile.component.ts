import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-department-profile',
  templateUrl: './department-profile.component.html',
  styleUrls: ['./department-profile.component.css'],
})
export class DepartmentProfileComponent implements OnInit {
  data: any;
  id: any;
  dob: any;
  endpoint="Department";

  constructor( private router: ActivatedRoute,private employeeService:EmployeeService) {}

  ngOnInit(): void {
    this.router.params.subscribe((params) => {
      this.id = params['id'];
    });

    this.employeeService.GetById(this.endpoint,this.id).subscribe((data: any) => {
      this.data = data;
      console.log(data);
    });
  }
}
