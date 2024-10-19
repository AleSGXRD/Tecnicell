import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessoryElementComponent } from './accessory-element.component';

describe('AccessoryElementComponent', () => {
  let component: AccessoryElementComponent;
  let fixture: ComponentFixture<AccessoryElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccessoryElementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccessoryElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
