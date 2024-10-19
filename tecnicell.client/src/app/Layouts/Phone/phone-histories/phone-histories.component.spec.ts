import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneHistoriesComponent } from './phone-histories.component';

describe('PhoneHistoriesComponent', () => {
  let component: PhoneHistoriesComponent;
  let fixture: ComponentFixture<PhoneHistoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PhoneHistoriesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PhoneHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
