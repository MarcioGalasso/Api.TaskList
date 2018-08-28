import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IUser } from '../../../services/base/auth/user';
import { AuthService } from '../../../services/base/auth/auth.service';


@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public user: IUser;

  constructor( 
    private authService: AuthService,
    
  ) {}

  ngOnInit() {      
    let user: IUser = JSON.parse(localStorage.getItem("user"));      
    if(user === null){
      user = {userName: "", password: "", remember: false};        
    }

    this.user = user;
  }

  onSubmit(frm : NgForm) {
    if (frm.valid) {
      this.authService.login(this.user);
    }
    else{
      // this.alertService.clear();s
      // this.alertService.error("Preencha todos os campos.");
    }      
  }
}