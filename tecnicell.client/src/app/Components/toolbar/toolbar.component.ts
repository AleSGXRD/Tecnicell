import { Component, EventEmitter, output, Output } from '@angular/core';
import { Rounded } from '../buttons/button/button.component';
import { AuthService } from '../../Services/api/Authorization/auth.service';
import { UserResponse } from '../../Interfaces/business/Authorization/UserResponse';
import { Route, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserInfo } from '../../Interfaces/business/Models/UserAccount';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.css'
})
export class ToolbarComponent {
  buttonRounded : Rounded = Rounded.FULL;
  user$! : Observable<UserInfo>;

  constructor(
    private authService: AuthService,
    private router : Router
  ){
    authService.loadUser();
    this.user$ = authService.myUser;
  }

  @Output() openSideBar = new EventEmitter<any>();
  @Output() openFormAdd = new EventEmitter<any>();

  logout(){
    localStorage.removeItem('user');
    this.router.navigate(["/login"]);
  }
}
