import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHandler } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { IUser } from './user';
import { Observable } from 'rxjs/Observable';



@Injectable()
export class AuthService  {

  private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  

  get isLoggedIn() {
    debugger;
    if (localStorage.getItem('token') !== '' && localStorage.getItem('token') !== null)
        this.loggedIn.next(true);
    else
        this.loggedIn.next(false);

    return this.loggedIn.asObservable();
    
  }

  

  constructor(private router: Router, private _http: HttpClient) {

  }
  
  loginAuto(user: IUser){
    let url: string = "api/token";
    localStorage.clear;

    return this._http.post(url,{
      "login": user.userName,
      "password": encodeURIComponent(user.password)
    })
    .subscribe(
      res => {
        localStorage.setItem("token", res["token"]);
        localStorage.setItem("user", JSON.stringify(user));
        this.loggedIn.next(true);        
      },
      err => {
        this.logout();      
      }
    );
  }

  login(user: IUser){     
    var url = "http://localhost:53540/api/token";
    
    localStorage.clear();
    
    var x = this._http.post(url,{
      "login": user.userName,
      "password": encodeURIComponent(user.password)
    })
    .subscribe(
      res => {
        localStorage.setItem("token", res["token"]);
        localStorage.setItem("user", JSON.stringify(user));        
        this.loggedIn.next(true);
        this.router.navigate(['/']);
       
      },
      err => {
        // this.alertService.clear();
        // this.alertService.error("Usuário e senha inválidos.");        
      }
    );

  }


  logout(){
    let user: IUser = JSON.parse(localStorage.getItem("user"));
    localStorage.clear();

    if(user != null && user.remember){
      localStorage.setItem("user", JSON.stringify(user));
    }

    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }

  private handleError(err: HttpErrorResponse) {
      console.log(err.message);
      return Observable.throw(err.message);
  }
}
