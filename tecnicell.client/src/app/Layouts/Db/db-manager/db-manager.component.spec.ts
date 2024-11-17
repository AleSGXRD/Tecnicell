import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DbManagerComponent } from './db-manager.component';

describe('DbManagerComponent', () => {
  let component: DbManagerComponent;
  let fixture: ComponentFixture<DbManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DbManagerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DbManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
