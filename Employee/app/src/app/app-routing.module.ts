import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeeProfileComponent} from './employees/employee-profile/employee-profile.component';
import { CreateEmployeeComponent} from './employees/create-employee/create-employee.component';
import { EditEmployeeComponent } from './employees/edit-employee/edit-employee.component';
import { OrganisationComponent } from './organisation/organisation.component';
import { OrganisationProfileComponent } from './organisation/organisation-profile/organisation-profile.component';
import { CreateOrganisationComponent } from './organisation/create-organisation/create-organisation.component';
import { EditOrganisationComponent } from './organisation/edit-organisation/edit-organisation.component';
import { DepartmentComponent } from './department/department.component';
import { DepartmentProfileComponent } from './department/department-profile/department-profile.component';
import { CreateDepartmentComponent } from './department/create-department/create-department.component';
import { EditDepartmentComponent } from './department/edit-department/edit-department.component';
import { DesignationComponent } from './designation/designation.component';
import { DesignationProfileComponent } from './designation/designation-profile/designation-profile.component';
import { CreateDesignationComponent } from './designation/create-designation/create-designation.component';
import { EditDesignationComponent } from './designation/edit-designation/edit-designation.component';
import { LoginComponent } from './login/login.component';
const routes: Routes = [

  {path:'employee-profile/:id', component:EmployeeProfileComponent},
  {path:'create-employee',component:CreateEmployeeComponent},
  {path:'edit-employee/:id',component:EditEmployeeComponent},

  {path:'organisation',component:OrganisationComponent},
  {path:'organisation-profile/:id',component:OrganisationProfileComponent},
  {path:'create-organisation',component:CreateOrganisationComponent},
  {path:'edit-organisation/:id',component:EditOrganisationComponent},

  {path:'department',component:DepartmentComponent},
  {path:'department-profile',component:DepartmentProfileComponent},
  {path:'create-department',component:CreateDepartmentComponent},
  {path:'edit-department/:id',component:EditDepartmentComponent},

  {path:'designation',component:DesignationComponent},
  {path:'designation-profile',component:DesignationProfileComponent},
  {path:'create-designation',component:CreateDesignationComponent},
  {path:'edit-designation/:id',component:EditDesignationComponent},
  {path:'employees', component: EmployeesComponent },
  {path:'',component:LoginComponent}, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
