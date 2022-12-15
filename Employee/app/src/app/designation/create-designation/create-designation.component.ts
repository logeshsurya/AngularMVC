import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-create-designation',
  templateUrl: './create-designation.component.html',
  styleUrls: ['./create-designation.component.css']
})
export class CreateDesignationComponent implements OnInit {
 
  error: any='';
  data:any;
  endpoint= 'Designation';


  constructor( private router: Router, private toastService: HotToastService,private employeeService:EmployeeService) { }
  Designation: any = {
    id: 0,
    designation_Name: '',
  }

  
  ngOnInit(): void {
   
  }

  OnSubmit() {
    this.employeeService.Add(this.endpoint, this.Designation).subscribe({
      next: (res) => { res ? this.showToast() : null },
      error: (error) => this.error = error.error.message
    });

  }
  showToast() {
    this.toastService.success('Designation added Successfully!',
      {
        autoClose: true,
        dismissible: true,
        icon: '❎',
      })
    this.router.navigate(['/designation']);
  }
}
