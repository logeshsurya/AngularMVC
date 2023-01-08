import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Observable} from "rxjs";
import { Employee } from './employees/employees';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  mvcUrl =  'https://localhost:7256/';
  constructor(private http:HttpClient) {}

   headers = new HttpHeaders().set('Authorization',`Bearer ${LoginService.GetData("token")}`);
  


   GetAll(endpoint:string):Observable<any>
   {
    
    console.log(this.headers);
    return this.http.get<any>(this.mvcUrl + endpoint + '/GetAll',{headers:this.headers} );
  }
  GetAllEmployee(endpoint:string,pageNo:any,size:any,sort: any):Observable<any>
  {
    this.mvcUrl = 'https://localhost:7256/Employee/GetAll?pageNo=' +pageNo + '&size=' + size +'&sort=' +sort;
    return this.http.get<any>(this.mvcUrl);
  }
  GetAllCount():Observable<any>
  {
    this.mvcUrl = 'http://localhost:7256/Employee/GetAllCount';
    return this.http.get(this.mvcUrl);
  }
  Add(endpoint:any, data:any):Observable<any>
  {
    return this.http.post<any>(this.mvcUrl + endpoint + '/Create',data);
  }
  GetById(endpoint:any,id:any)
  {
    return this.http.get<any>(this.mvcUrl+endpoint+ `/GetById?id=${id}`)
  }
  Delete(endpoint:any,id:any):Observable<Employee[]>
  {
    return this.http.post<any>(this.mvcUrl + endpoint + `/Delete?id=${id}`,id)
  }
  Edit(endpoint:any,data:any):Observable<Employee[]>
  {
    return this.http.put<any>(this.mvcUrl + endpoint + '/Update',data);
  }
  count()
  {
    return this.http.get<any>(this.mvcUrl + '/Employee/getcount');
  }
}





 // GetAllEmployee(endpoint:string,pageNo:any,size:any,sort: any):Observable<any>
  // {

  //   this.mvcUrl = 'https://localhost:7031/api/ApiEmployee/GetAll?pageNo=' +pageNo + '&size=' + size +
  //   '&sort=' +sort;
  //   return this.http.get<any>(this.mvcUrl);
  // }
