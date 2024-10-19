import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Notification, NotificationType } from '../Components/notification-bubble/notification-bubble.component';

@Injectable({
  providedIn: 'root'
})
export class NotificationSystemService {

  notification : BehaviorSubject<Notification> = new BehaviorSubject<Notification>({
    message : '',
    type : NotificationType.SUCCESS
  })
  
  showNotifcation(message: string, type: NotificationType){
    const value : Notification = {
      message : message,
      type : type
    }
    this.notification.next(value);
  }
  showNotifcationWithError(message: string, type: NotificationType, err: any){
    const value : Notification = {
      message : message,
      type : type
    }
    console.log(err);
    this.notification.next(value);
  }
}
