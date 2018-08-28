import { Component, OnInit, Input, SimpleChanges, HostListener } from '@angular/core';
import { BasePaginatorListComponent } from './basePaginatorListComponent';
import { HttpClient } from '@angular/common/http';
import { IBaseModel } from '../../../base/interface/ibaseModel';

@Component({
  selector: 'ck-paginator',
  templateUrl: './ck-paginator.component.html',
  styleUrls: ['./ck-paginator.component.css']
})

export class CkPaginatorComponent  extends BasePaginatorListComponent implements OnInit {
  
  public record: number;
  public useFilter : boolean = false;
  private hiddenFilter : boolean = true;
  private hiddenOverflow :boolean = true;

  @Input() size : number;
  @Input() listRegister : Array<IBaseModel>;
  @Input() controller : string;
  @Input() onLoadRegister : boolean;
  @Input() useCache : boolean = false;
  @Input() filter : Array<Filter>; 
  
  constructor(http: HttpClient) 
  {
    super(http);
   
  }

  ngOnInit() {
   
    this.paginate.size = this.size;
    this.useFilter = this.filter && this.filter.length > 0;
    if(this.onLoadRegister){
      this.paginate.paginateFilter = null;
       this.onPaginate();
    }
   
  }

  public showF(){
   
    this.hiddenOverflow = !this.hiddenOverflow; 
    this.hiddenFilter = !this.hiddenFilter;
  }

  @HostListener('document:click', ['$event'])
  closeFilter(event) {
    if(event.srcElement.id == "overflow" && !this.hiddenFilter){
        this.showF()
    }
  }
  public searchEmit  (paginate)
   {
      this.showF()
      this.resetProperty();
      this.paginate = paginate;
      this.paginate.size = this.size;
      this.onPaginate();
  }

}
export class Filter {
  public type : string;
  public column : string;
  public title : string;
  public condition : string;
  public optionList : Array<object>;
}

