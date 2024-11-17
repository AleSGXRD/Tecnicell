import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { themes } from '../../public/theme';
import { NotificationSystemService } from './Services/notification-system.service';
import { Notification } from './Components/notification-bubble/notification-bubble.component';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent  {

  title = 'tecnicell.client';
  themes = themes;

  notification! : Notification;
  
  constructor(private notificationService : NotificationSystemService){
    notificationService.notification.subscribe(res => this.notification = res);
  }
}
