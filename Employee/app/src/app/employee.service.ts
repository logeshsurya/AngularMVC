import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {  Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  readonly mvcUrl = "https://localhost:7256/";
  constructor(private http:HttpClient ) { }

  Add(endpoint:any, data:any):Observable<any>
  {
    return this.http.post<any>(this.mvcUrl + endpoint + '/Create',data);
  }
  GetEmployeeById(endpoint:any,id:any)
  {
    return this.http.get<any>(this.mvcUrl+endpoint+ `/GetEmployeeById?id=${id}`)
  }
  Delete(endpoint:any,id:any):Observable<any>
  {
    return this.http.post<any>(this.mvcUrl + endpoint + `/Delete?id=${id}`,Object)
  }
  Edit(endpoint:any,data:any):Observable<any>
  {
    return this.http.put<any>(this.mvcUrl + endpoint + '/Update',data);
  }

}