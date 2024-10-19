import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatteryBrandsTableComponent } from './battery-brands-table.component';

describe('BatteryBrandsTableComponent', () => {
  let component: BatteryBrandsTableComponent;
  let fixture: ComponentFixture<BatteryBrandsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatteryBrandsTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BatteryBrandsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
