import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreensTableComponent } from './screens-table.component';

describe('ScreensTableComponent', () => {
  let component: ScreensTableComponent;
  let fixture: ComponentFixture<ScreensTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ScreensTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScreensTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
