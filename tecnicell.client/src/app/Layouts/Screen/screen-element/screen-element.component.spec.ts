import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreenElementComponent } from './screen-element.component';

describe('ScreenElementComponent', () => {
  let component: ScreenElementComponent;
  let fixture: ComponentFixture<ScreenElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ScreenElementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScreenElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
