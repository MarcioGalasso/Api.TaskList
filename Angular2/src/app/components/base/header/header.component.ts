import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../../../services/base/auth/auth.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;

  constructor(private _auth: AuthService) {

  }
    
  ngOnInit() {
    this.isLoggedIn$ = this._auth.isLoggedIn;
  }

  onLogout(): void{
    this._auth.logout();    
  }   
  
  toggle(): void{
    
  }
}
