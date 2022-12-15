import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EmployeeService } from '../employee.service';


@Component({
  selector: 'app-organisation',
  templateUrl: './organisation.component.html',
  styleUrls: ['./organisation.component.css']
})
export class OrganisationComponent implements OnInit {
  endpoint = "Organisation";
  totalLength: any;
  page: number = 1;
  searchValue = '';
  public filteredData: any[] = [];
  organisationname: any;
  data:any;


  constructor(private employeeService:EmployeeService) { }

  ngOnInit(): void {
    this.employeeService.GetAll(this.endpoint).subscribe(data => {
      this.data = data;
      this.filteredData = data;
      this.totalLength = data;
      console.log(data);
    });

  }



  deleteOrganisation(id:any)
  {
    this.employeeService.Delete(this.endpoint,id).subscribe((result)=>
    {
      console.log(result);
      this.ngOnInit();
    })
  }

  Search(value: string) {
    this.data = this.filteredData.filter(item =>
      item.organisationName.toLowerCase().includes(value.toLowerCase()))
    this.page = 1;
  }


  

}

