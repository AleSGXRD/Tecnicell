import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAccountTableComponent } from './user-account-table.component';

describe('UserAccountTableComponent', () => {
  let component: UserAccountTableComponent;
  let fixture: ComponentFixture<UserAccountTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserAccountTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserAccountTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
