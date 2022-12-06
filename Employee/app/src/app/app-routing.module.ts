import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './employee/employee.component';
import{EmployeeProfileComponent} from './employee-profile/employee-profile.component';
import {CreateEmployeeComponent} from './create-employee/create-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
const routes: Routes = [

  { path: '', component: EmployeeComponent },
  {path:'employee-profile/:id', component:EmployeeProfileComponent},
  {path:'create-employee',component:CreateEmployeeComponent},
  {path:'edit-employee/:id',component:EditEmployeeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
