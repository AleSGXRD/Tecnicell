import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneRepairsTableComponent } from './phone-repairs-table.component';

describe('PhoneRepairsTableComponent', () => {
  let component: PhoneRepairsTableComponent;
  let fixture: ComponentFixture<PhoneRepairsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PhoneRepairsTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PhoneRepairsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
