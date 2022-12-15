import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-designation-profile',
  templateUrl: './designation-profile.component.html',
  styleUrls: ['./designation-profile.component.css'],
})
export class DesignationProfileComponent implements OnInit {
  data: any;
  id: any;
  dob: any;
  endpoint="Designation"

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
