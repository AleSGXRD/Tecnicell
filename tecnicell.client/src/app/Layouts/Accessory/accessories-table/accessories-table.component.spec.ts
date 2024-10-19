import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessoriesTableComponent } from './accessories-table.component';

describe('AccessoriesTableComponent', () => {
  let component: AccessoriesTableComponent;
  let fixture: ComponentFixture<AccessoriesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccessoriesTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccessoriesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
