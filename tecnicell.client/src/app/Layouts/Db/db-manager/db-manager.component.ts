import { Component } from '@angular/core';
import { DbApiService } from '../../../Services/api/Db/db-api.service';

@Component({
  selector: 'app-db-manager',
  templateUrl: './db-manager.component.html',
  styleUrl: './db-manager.component.css'
})
export class DbManagerComponent {
  direction : string[]=['Main', "Base de Datos"];

  constructor(private dbApi: DbApiService){}

  saveDb(){
    this.dbApi.saveDb();
  }
}
