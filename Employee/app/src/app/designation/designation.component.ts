import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EmployeeService } from '../employee.service';



@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.css']
})
export class DesignationComponent implements OnInit {
  totalLength: any;
  page: number = 1;
  endpoint = "Designation";
  searchValue = '';
  public filteredData: any[] = [];
  designationname: any;
  data:any;
  id:any;


  constructor(private employeeService:EmployeeService) { }



  ngOnInit(): void {
 
    this.employeeService.GetAll(this.endpoint).subscribe(data => {
      this.data = data;
      this.filteredData = data;
      this.totalLength = data;

    });
  }

  deleteDesignation(id:any)
    {
      this.employeeService.Delete(this.endpoint,id).subscribe((result)=>
      {
        console.log(result);
        this.ngOnInit();
      })
    }

  Search(value: string) {
    this.data = this.filteredData.filter(item =>
      item.designationName.toLowerCase().includes(value.toLowerCase()))
    this.page = 1;
  }



}
