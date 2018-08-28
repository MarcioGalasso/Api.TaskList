import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CkPaginatorComponent } from './ck-paginator.component';

describe('CkPaginatorComponent', () => {
  let component: CkPaginatorComponent;
  let fixture: ComponentFixture<CkPaginatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CkPaginatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CkPaginatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
