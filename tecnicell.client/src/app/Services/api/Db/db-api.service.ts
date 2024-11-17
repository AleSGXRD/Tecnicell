import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import server from '../../../Logic/ServerAdress';
import { NotificationSystemService } from '../../notification-system.service';

@Injectable({
  providedIn: 'root'
})
export class DbApiService {

  constructor(public http: HttpClient, private notificationService: NotificationSystemService) {}

  saveDb(){
    this.http.post(server() + '/api/db', '').subscribe(
      (res:any) => this.notificationService.showNotifcation(res['message'], 0),
      (err:any) => this.notificationService.showNotifcation(err['message'], 1)
    );
  }
}
