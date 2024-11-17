import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreformanceComponent } from './preformance.component';

describe('PreformanceComponent', () => {
  let component: PreformanceComponent;
  let fixture: ComponentFixture<PreformanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreformanceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PreformanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
