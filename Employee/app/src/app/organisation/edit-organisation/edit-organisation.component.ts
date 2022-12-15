import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { EmployeeService } from 'src/app/employee.service';

@Component({
  selector: 'app-edit-organisation',
  templateUrl: './edit-organisation.component.html',
  styleUrls: ['./edit-organisation.component.css']

})
export class EditOrganisationComponent implements OnInit {


  endpoint = "Organisation";
  id: any;
  data: any;
  error: any = '';
  
  constructor( private route: ActivatedRoute,
     private routing: Router, private toastService: HotToastService,
     private employeeservice:EmployeeService) { }

  ngOnInit(): void {
    
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.employeeservice.GetById(this.endpoint, this.id).subscribe((data) => {
        this.data = data;
      });
    });
  }

  OnSubmit() {
    this.employeeservice.Edit(this.endpoint, this.data).subscribe({
      next: (data) => { data ? this.showToast() : null },
      error: (error) => this.error = error.error.message
    });
  }
  showToast() {
    this.toastService.success('Organisation updated Successfully!',
      {
        autoClose: true,
        dismissible: true,
      })
    this.routing.navigate(['/organisation']);
  }
}

