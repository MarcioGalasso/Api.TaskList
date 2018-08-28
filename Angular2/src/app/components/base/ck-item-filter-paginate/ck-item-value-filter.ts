import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'ck-item-text-paginate',
  template: "<div class='col-md-12'>"
              +" <mat-form-field class='col-md-12'>"
               +"<mat-label>Valor</mat-label>"
               +"<input name='value'  [(ngModel)]='valueFilter.value' required matInput >"
            +"</mat-form-field>"
            +"</div>"
})

export class CkItemTextPaginateComponent implements OnInit {

  @Input() valueFilter : object;
  constructor() { 

  }

  ngOnInit() {
    
  }

}


@Component({
  selector: 'ck-item-number-paginate',
  template: "<div class='col-md-12'>"
           + " <mat-form-field class='col-md-12' >"
               +"<mat-label>Valor</mat-label>"
               +"<input type='number' [(ngModel)]='valueFilter.value' name='value' required matInput >"
            +"</mat-form-field>"
            +"</div>"
})

export class CkItemNumberPaginateComponent implements OnInit {
  @Input() valueFilter : object;
  constructor() { 

  }

  ngOnInit() {
    
  }

}

@Component({
  selector: 'ck-item-date-paginate',
  template: "<div class='col-md-12'>"
           + " <mat-form-field class='col-md-12' >"
               +"<mat-label>Valor Date</mat-label>"
               +"<input type='date' [(ngModel)]='valueFilter.value' name='value' required matInput >"
            +"</mat-form-field>"
            +"</div>"
})
export class CkItemDatePaginateComponent implements OnInit {
  @Input() valueFilter : object;
  constructor() { 

  }

  ngOnInit() {
    
  }
}

@Component({
  selector: 'ck-item-select-box-paginate',
  template: "<div class='col-md-12'>"
            +  "<mat-form-field class='col-md-12'>"
                  +"<mat-label>Valor</mat-label>"
                        +"<mat-select   [(ngModel)]='valueFilter.value' >"
                        + " <mat-option *ngFor='let item of optionList;' [value]='item.value'>{{item.title}}</mat-option>"
                        +"</mat-select>"
             +"</mat-form-field>"
             +"</div>"
})

export class CkItemSelectBoxPaginateComponent implements OnInit {
  @Input() valueFilter : object;
  @Input() optionList : Array<object>;
  constructor() { 

  }

  ngOnInit() {
    
  }

}


