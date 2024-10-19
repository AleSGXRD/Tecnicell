import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessoryHistoriesComponent } from './accessory-histories.component';

describe('AccessoryHistoriesComponent', () => {
  let component: AccessoryHistoriesComponent;
  let fixture: ComponentFixture<AccessoryHistoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccessoryHistoriesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccessoryHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
