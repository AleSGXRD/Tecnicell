import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../Services/api/Authorization/auth.service';
import checkLevel, { AccessLevel } from '../../Logic/AccessLevel';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  appear:boolean = false;
  access!:AccessLevel;

  constructor(private authService : AuthService,
  ){
    authService.myUser.subscribe(res=>{
      this.access = checkLevel(res.role);
    })
  }
}
