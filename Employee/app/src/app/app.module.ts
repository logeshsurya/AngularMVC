import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { AppRoutingModule } from './app-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { AppComponent } from './app.component';

import { EmployeeProfileComponent } from './employees/employee-profile/employee-profile.component';
import { CreateEmployeeComponent } from './employees/create-employee/create-employee.component';
import { EditEmployeeComponent } from './employees/edit-employee/edit-employee.component';
import { EmployeesComponent } from './employees/employees.component';

import { CreateDepartmentComponent } from './department/create-department/create-department.component';
import { EditDepartmentComponent } from './department/edit-department/edit-department.component';
import { DepartmentProfileComponent } from './department/department-profile/department-profile.component';
import { DepartmentComponent } from './department/department.component';

import { CreateDesignationComponent } from './designation/create-designation/create-designation.component';
import { EditDesignationComponent } from './designation/edit-designation/edit-designation.component';
import { DesignationProfileComponent } from './designation/designation-profile/designation-profile.component';
import { DesignationComponent } from './designation/designation.component';

import { OrganisationProfileComponent } from './organisation/organisation-profile/organisation-profile.component';
import { EditOrganisationComponent } from './organisation/edit-organisation/edit-organisation.component';
import { CreateOrganisationComponent } from './organisation/create-organisation/create-organisation.component';
import { OrganisationComponent } from './organisation/organisation.component';
import { LoginComponent } from './login/login.component';

import { AgGridModule } from 'ag-grid-angular';
import { ToastrModule } from 'ngx-toastr';
import { ModuleRegistry } from '@ag-grid-community/core';   
import { ClientSideRowModelModule } from "@ag-grid-community/client-side-row-model";
import {RangeSelectionModule} from "@ag-grid-enterprise/range-selection";

ModuleRegistry.registerModules([
  ClientSideRowModelModule,
  RangeSelectionModule
]);

@NgModule({
  declarations: [
    AppComponent,
    EmployeesComponent,
    EmployeeProfileComponent,
    CreateEmployeeComponent,
    EditEmployeeComponent,
    CreateDepartmentComponent,
    DepartmentProfileComponent,
    EditDepartmentComponent,
    CreateDesignationComponent,
    EditDesignationComponent,
    DesignationProfileComponent,
    OrganisationProfileComponent,
    EditOrganisationComponent,
    CreateOrganisationComponent,
    EmployeesComponent,
    DepartmentComponent,
    DesignationComponent,
    OrganisationComponent,
    LoginComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    FormsModule,
    NgxPaginationModule,
    Ng2SearchPipeModule,
    AgGridModule,
    ToastrModule.forRoot(),
    ], 
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
