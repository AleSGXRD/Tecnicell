import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatteryHistoriesComponent } from './battery-histories.component';

describe('BatteryHistoriesComponent', () => {
  let component: BatteryHistoriesComponent;
  let fixture: ComponentFixture<BatteryHistoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatteryHistoriesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BatteryHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
