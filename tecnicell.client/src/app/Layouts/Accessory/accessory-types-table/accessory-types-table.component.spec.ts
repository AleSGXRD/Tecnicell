import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessoryTypesTableComponent } from './accessory-types-table.component';

describe('AccessoryTypesTableComponent', () => {
  let component: AccessoryTypesTableComponent;
  let fixture: ComponentFixture<AccessoryTypesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccessoryTypesTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccessoryTypesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
