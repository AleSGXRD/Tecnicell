import { Component, Input } from '@angular/core';
import { NotificationSystemService } from '../../Services/notification-system.service';

@Component({
  selector: 'app-notification-bubble',
  templateUrl: './notification-bubble.component.html',
  styleUrl: './notification-bubble.component.css'
})
export class NotificationBubbleComponent {
  @Input()
  notification : Notification = {
    message :  "Ha ocurrido un error al entrar los datos",
    type : NotificationType.WARNING
  }

}

export interface Notification{
  message :string,
  type : NotificationType
}

export enum NotificationType{
  SUCCESS = 0,
  WARNING = 1
}