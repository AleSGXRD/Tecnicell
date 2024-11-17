import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchsTableComponent } from './searchs-table.component';

describe('SearchsTableComponent', () => {
  let component: SearchsTableComponent;
  let fixture: ComponentFixture<SearchsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchsTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
