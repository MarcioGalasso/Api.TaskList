import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CkFilterPaginateComponent } from '../ck-filter-paginate/ck-filter-paginate.component';
import { CkItemFilterPaginateComponent } from '../ck-item-filter-paginate/ck-item-filter-paginate.component';
import { CkItemTextPaginateComponent, CkItemSelectBoxPaginateComponent, CkItemNumberPaginateComponent, CkItemDatePaginateComponent } from '../ck-item-filter-paginate/ck-item-value-filter';
import { CkPaginatorComponent } from './ck-paginator.component';
import {  MatRadioModule,  MatPaginatorModule, MatSelectModule, MatInputModule } from '@angular/material';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    FormsModule,
    MatPaginatorModule,
    CommonModule,
    MatSelectModule,
    MatInputModule,
    MatRadioModule
  ],
  exports:[
    CkFilterPaginateComponent,
    CkItemFilterPaginateComponent,
    CkItemTextPaginateComponent,
    CkItemSelectBoxPaginateComponent,
    CkItemNumberPaginateComponent,
    CkPaginatorComponent,
  ],
  declarations: [
    CkFilterPaginateComponent,
    CkItemFilterPaginateComponent,
    CkItemTextPaginateComponent,
    CkItemSelectBoxPaginateComponent,
    CkItemNumberPaginateComponent,
    CkPaginatorComponent,
    CkItemDatePaginateComponent
  ],
  entryComponents: [CkItemFilterPaginateComponent,CkItemTextPaginateComponent,CkItemNumberPaginateComponent,CkItemSelectBoxPaginateComponent,CkItemDatePaginateComponent]
})
export class CkPaginatorModule { }
