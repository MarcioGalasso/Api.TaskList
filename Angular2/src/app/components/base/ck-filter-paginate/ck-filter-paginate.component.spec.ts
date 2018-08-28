import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CkFilterPaginateComponent } from './ck-filter-paginate.component';

describe('CkFilterPaginateComponent', () => {
  let component: CkFilterPaginateComponent;
  let fixture: ComponentFixture<CkFilterPaginateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CkFilterPaginateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CkFilterPaginateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
