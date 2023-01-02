import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor() { }
  static GetData(key:string):string|null 
  {
    const itemstr = localStorage.getItem(key)

    if(!itemstr) 
    {
      return null;
    }
    const item = JSON.parse(itemstr)
    const now = new Date()

    if(now.getTime() > item.expiry)
    {
      localStorage.removeItem(key)
      return null
    }
    
    return item.value
  }

  static SetDateWithExpiry(key:string, value:string,expiryInMinutes:number)
  {
    const now = new Date()
    expiryInMinutes = expiryInMinutes * 60000;

    const item = {
      value :value,
      expiry :now.getTime() + expiryInMinutes,
    }

    localStorage.setItem(key,JSON.stringify(item))
  }


  static Logout()
  {
    localStorage.clear();
  }
}
