import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EmployeeService } from '../employee.service';


@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
  totalLength: any;
  page: number = 1;
  endpoint = "Department";
  searchValue = '';
  public filteredData: any[] = [];
  departmentname: any;
  data: any;
  id:any;

  constructor(private employeeService:EmployeeService) { }

  ngOnInit(): void {
    this.employeeService.GetAll(this.endpoint).subscribe(data => {
      this.data = data;
      this.filteredData = data;
      console.log(data);
    });
  }


  deleteDepartment(id:any)
  {
    this.employeeService.Delete(this.endpoint,id).subscribe((result)=>
    {
      console.log(result);
      this.ngOnInit();
    })
  }

  Search(value: string) {
    this.data = this.filteredData.filter(item =>
      item.departmentName.toLowerCase().includes(value.toLowerCase()))
    this.page = 1;
  }



}
