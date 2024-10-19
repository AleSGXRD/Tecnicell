import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreenHistoriesComponent } from './screen-histories.component';

describe('ScreenHistoriesComponent', () => {
  let component: ScreenHistoriesComponent;
  let fixture: ComponentFixture<ScreenHistoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ScreenHistoriesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScreenHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
