import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CkItemFilterPaginateComponent } from './ck-item-filter-paginate.component';

describe('CkItemFilterPaginateComponent', () => {
  let component: CkItemFilterPaginateComponent;
  let fixture: ComponentFixture<CkItemFilterPaginateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CkItemFilterPaginateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CkItemFilterPaginateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
