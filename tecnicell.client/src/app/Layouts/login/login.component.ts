import { Component, signal } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../Services/api/Authorization/auth.service';
import { AuthRequest } from '../../Interfaces/business/Authorization/AuthRequest';
import { Router } from '@angular/router';
import { NotificationSystemService } from '../../Services/notification-system.service';
import { MyErrorStateMatcher } from '../../Components/form/form.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  form: any = this.formBuilder.group({
    name: ['', [Validators.required]],
    password : ['', [Validators.required]]
  })
  matcher = new MyErrorStateMatcher();

  constructor(private formBuilder : FormBuilder,
    private authApi : AuthService,
    private router: Router,
    private notificationService: NotificationSystemService
  ){

  }
  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
  
  submit(){
    let user : AuthRequest = {
      name : this.form.value.name,
      password : this.form.value.password
    }
    this.authApi.auth(user).subscribe(
      res=>{
        console.log(res);
        if(res.success == 0) return;

        window.localStorage.setItem("user", JSON.stringify(res.user))
        this.notificationService.showNotifcation("Se ha iniciado sesión con exito!", 0)
        this.router.navigate(['/'])
      },
      err =>{
        this.notificationService.showNotifcation("Usuario o contraseña incorrectos", 1)
      }
    );
  }
}
