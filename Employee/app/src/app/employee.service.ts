import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {  Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  readonly mvcUrl = "https://localhost:7256/";
  constructor(private http:HttpClient ) { }

  GetAll(endpoint:string):Observable<any>
  {
    return this.http.get<any>(this.mvcUrl + endpoint + '/GetAll');
  }

  Add(endpoint:any, data:any):Observable<any>
  {
    return this.http.post<any>(this.mvcUrl + endpoint + '/Create',data);
  }
  GetById(endpoint:any,id:any)
  {
    return this.http.get<any>(this.mvcUrl+endpoint+ `/GetById?id=${id}`)
  }
  Delete(endpoint:any,id:any):Observable<any>
  {
    return this.http.post<any>(this.mvcUrl + endpoint + `/Delete?id=${id}`,id)
  }
  Edit(endpoint:any,data:any):Observable<any>
  {
    return this.http.put<any>(this.mvcUrl + endpoint + '/Update',data);
  }
  // getproductfilters(id:any):Observable<any>{

  //   console.warn("product",id);

  //   return this.http.get<any>(`https://localhost:7143/product/filters?id=${id}`,id);

  // }

}
