import { Injectable } from '@angular/core';
import { BehaviorSubject, filter } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterTableService {

  public filters : BehaviorSubject<any> = new BehaviorSubject(null);
  public values : BehaviorSubject<any> = new BehaviorSubject([]);

  constructor() { }

  emit(values : any){
    this.filters.next(values);
  }
}
