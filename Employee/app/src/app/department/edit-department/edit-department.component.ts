import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-edit-department',
  templateUrl: './edit-department.component.html',
  styleUrls: ['./edit-department.component.css']
})
export class EditDepartmentComponent implements OnInit {
  id: any = 0;
  data: any;
  error: any='';
  endpoint = "Department";



  constructor( private router: ActivatedRoute, private routing: Router, 
    private toastService: HotToastService,private employeeService:EmployeeService) { }
  ngOnInit(): void {
  

    this.router.params.subscribe(params => {
      this.id = params['id'];
      this.employeeService.GetById(this.endpoint, this.id).subscribe((result) => {
        this.data = result;
      });
    });
   


  }

  OnSubmit() {
    this.employeeService.Edit(this.endpoint, this.data).subscribe({
      next: (res) => { res ? this.showToast() : null },
      error: (error) => this.error = error.error.message
    });
  }
  showToast() {
    this.toastService.success('Department updated Successfully!',
      {
        autoClose: true,
        dismissible: true,
      })
    this.routing.navigate(['/department']);

  }
}
