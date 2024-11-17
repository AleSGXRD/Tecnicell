import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkTypesTableComponent } from './work-types-table.component';

describe('WorkTypesTableComponent', () => {
  let component: WorkTypesTableComponent;
  let fixture: ComponentFixture<WorkTypesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WorkTypesTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkTypesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
