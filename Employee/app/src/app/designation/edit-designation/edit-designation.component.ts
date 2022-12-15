import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { EmployeeService } from 'src/app/employee.service';


@Component({
  selector: 'app-edit-designation',
  templateUrl: './edit-designation.component.html',
  styleUrls: ['./edit-designation.component.css']
})
export class EditDesignationComponent implements OnInit {


  error:any;
  id: any = 0;
  data: any;
  endpoint = "Designation";

  
  constructor( private router: ActivatedRoute, private routing: Router,
    private employeeService:EmployeeService, private toastService: HotToastService) { }

 
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
    this.toastService.success('Designation updated Successfully!',
      {
        autoClose: true,
        dismissible: true,
      })
    this.routing.navigate(['/designation']);

  }


}
