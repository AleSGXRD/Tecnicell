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
  deferredPrompt: any;

  constructor(private notificationService : NotificationSystemService){
    this.notificationService.notification.subscribe(res => this.notification = res);
  }

  ngOnInit() {
    window.addEventListener('beforeinstallprompt', (e) => {
      e.preventDefault(); // Prevenir que la mini barra de información aparezca
      this.deferredPrompt = e; // Guardar el evento para usarlo más tarde
      // Mostrar un botón u otro elemento para iniciar el flujo de instalación
      this.showInstallPromotion();
    });
  }
  showInstallPromotion() {
    if (this.deferredPrompt) {
      this.deferredPrompt.prompt();
      this.deferredPrompt.userChoice.then((choiceResult:any) => {
        if (choiceResult.outcome === 'accepted') {
          console.log('App instalada');
        } else {
          console.log('App no instalada');
        }
        this.deferredPrompt = null;
      });
    }
  }
}