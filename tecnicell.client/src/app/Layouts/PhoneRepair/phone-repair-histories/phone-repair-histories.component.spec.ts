import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneRepairHistoriesComponent } from './phone-repair-histories.component';

describe('PhoneRepairHistoriesComponent', () => {
  let component: PhoneRepairHistoriesComponent;
  let fixture: ComponentFixture<PhoneRepairHistoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PhoneRepairHistoriesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PhoneRepairHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
