import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneElementComponent } from './phone-element.component';

describe('PhoneElementComponent', () => {
  let component: PhoneElementComponent;
  let fixture: ComponentFixture<PhoneElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PhoneElementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PhoneElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
