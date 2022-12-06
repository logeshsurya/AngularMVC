import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-employee-profile',
  templateUrl: './employee-profile.component.html',
  styleUrls: ['./employee-profile.component.css'],
})
export class EmployeeProfileComponent implements OnInit {
  data: any;
  id: any;
  dob: any;

  constructor(private http: HttpClient, private router: ActivatedRoute) {}

  ngOnInit(): void {
    this.router.params.subscribe((params) => {
      this.id = params['id'];
    });

    this.getEmployeeById(this.id).subscribe((data: any) => {
      this.data = data;
      console.log(data);
    });
  }
  getEmployeeById(id: any): Observable<any> {
    return this.http.get<any>(
      'https://localhost:7256/Employee/GetEmployeeById' + `?id=${id}`
    );
  }
}
