import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhonesTableComponent } from './phones-table.component';

describe('PhonesTableComponent', () => {
  let component: PhonesTableComponent;
  let fixture: ComponentFixture<PhonesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PhonesTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PhonesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
