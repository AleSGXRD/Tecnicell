import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaryWorksTableComponent } from './diary-works-table.component';

describe('DiaryWorksTableComponent', () => {
  let component: DiaryWorksTableComponent;
  let fixture: ComponentFixture<DiaryWorksTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DiaryWorksTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DiaryWorksTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
