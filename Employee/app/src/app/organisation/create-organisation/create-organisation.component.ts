import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { EmployeeService } from 'src/app/employee.service';



@Component({
  selector: 'app-create-organisation',
  templateUrl: './create-organisation.component.html',
  styleUrls: ['./create-organisation.component.css']
})
export class CreateOrganisationComponent implements OnInit {

  error: any = '';
  data:any;
  endpoint = "Organisation";

  constructor( private router: Router, private toastService: HotToastService, private employeeService:EmployeeService) { }
  
  Organisation: any = {
    id: 0,
    organisationName: '',
  }

  ngOnInit(): void {


  }

  OnSubmit() {
    this.employeeService.Add(this.endpoint, this.Organisation).subscribe({
      next: (res) => { res ? this.showToast() : null },
      error: (error) => this.error = error.error.message
    })

  }
  showToast() {
    this.toastService.success('Organisation added Successfully!',
      {
        autoClose: true,
        dismissible: true,
      })
    this.router.navigate(['/organisation']);

  }
}


