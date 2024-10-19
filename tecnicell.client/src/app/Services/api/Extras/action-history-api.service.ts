import { Injectable } from '@angular/core';
import { ApiService } from '../ApiService.service';
import { ActionHistory } from '../../../Interfaces/business/Models/ActionHistory';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ActionHistoryApiService  {

  constructor(public http: HttpClient) {}

  select(): Observable<ActionHistory[]>{
    return this.http.get<ActionHistory[]>(environment.url + '/api/ActionHistories');
  }
}
