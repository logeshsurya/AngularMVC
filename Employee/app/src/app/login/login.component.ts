
import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import {  LoginService } from 'src/app/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  showErrorMessage = false;
  IsLoading: boolean = false;
  IsVerified: string = ''
  returnUrl: any;
  constructor(private http: HttpClient, private router: Router, private route:ActivatedRoute,private login:LoginService ) { }
  user: any = {

    email: '',
    password: '',

  }


  ngOnInit(): void {
  }

  OnSubmit() {

    this.IsLoading = true;
    this.showErrorMessage = false;
    const headers = { 'content-type': 'application/json' }

    this.http.post<any>('https://localhost:7031/api/Token', this.user)
      .subscribe({
        next: (data) => {
          console.log(data);
      
         // LoginService.SetDateWithExpiry("token", data.token, data.expiryInMinutes)
          //LoginService.SetDateWithExpiry("User", data.userId, data.expiryInMinutes)
    

          

            this.router.navigateByUrl("/employees").then(() => {
              window.location.reload();
            })
          
        },

        error: (error) => {

          this.showErrorMessage = true;
        }
      });

  }

}