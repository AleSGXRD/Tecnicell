import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatteriesTableComponent } from './batteries-table.component';

describe('BatteriesTableComponent', () => {
  let component: BatteriesTableComponent;
  let fixture: ComponentFixture<BatteriesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BatteriesTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BatteriesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
