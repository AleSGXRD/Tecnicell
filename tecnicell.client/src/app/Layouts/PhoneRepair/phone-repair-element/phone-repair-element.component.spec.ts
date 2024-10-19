import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneRepairElementComponent } from './phone-repair-element.component';

describe('PhoneRepairElementComponent', () => {
  let component: PhoneRepairElementComponent;
  let fixture: ComponentFixture<PhoneRepairElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PhoneRepairElementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PhoneRepairElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
