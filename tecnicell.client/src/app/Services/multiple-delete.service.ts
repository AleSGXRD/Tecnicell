import { EventEmitter, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MultipleDeleteService {
  public deleteMultiples = new EventEmitter();

  public canUseMultipleDelete = new BehaviorSubject<boolean>(false);
  public canDelete = new BehaviorSubject<boolean>(false);
  

  constructor() { }
}
