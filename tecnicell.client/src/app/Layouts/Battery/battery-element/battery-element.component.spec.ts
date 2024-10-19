import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatteryElementComponent } from './battery-element.component';

describe('BatteryElementComponent', () => {
  let component: BatteryElementComponent;
  let fixture: ComponentFixture<BatteryElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatteryElementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BatteryElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
