import {  OnInit, 
          Component,
          ViewChild,
          ViewContainerRef,
          ComponentFactoryResolver,
          ComponentRef,
          ComponentFactory, 
          Input,
          EventEmitter,
          Output} from '@angular/core';
          

import { CkItemFilterPaginateComponent } from '../ck-item-filter-paginate/ck-item-filter-paginate.component';
import { Filter } from '../ck-paginator/ck-paginator.component';
import { Paginate, Filters } from '../ck-paginator/paginate';



@Component({
  selector: 'ck-filter-paginate',
  templateUrl: './ck-filter-paginate.component.html',
  styleUrls: ['./ck-filter-paginate.component.css'],

})

export class CkFilterPaginateComponent implements OnInit {

  private conditionFilter : any; 
  componentRef: any;
  private listComponentRef  : Array<any> = [];
  public seasons : Array<object> = new Array<object>();

  @Input() propertySearch : Array<Filter>;
  @Output() paginateEvent = new EventEmitter();
  @ViewChild('containerItemFilter', { read: ViewContainerRef }) entry: ViewContainerRef;

  constructor(private resolver: ComponentFactoryResolver) { }

  ngOnInit() {
    this.seasons =  [{value:'and', title:"E"}, {value:'or', title: "OU"}];
    this.conditionFilter = this.seasons[0];
  }

  createComponent() {
   
    const factory = this.resolver.resolveComponentFactory(CkItemFilterPaginateComponent); 
    const componentRef = this.entry.createComponent(factory);
    this.listComponentRef.push(componentRef);
    componentRef.instance.propertySearch = this.propertySearch;
  } 

  private removeItemFilterDestroy () {
    this.listComponentRef.forEach(function(x,index){
      if(x.instance.destroy) {
        x.destroy();
        this.listComponentRef.splice(index);
      }
    },this);

  }

  public search () {
    debugger;
    var paginate = new Paginate();
    this.listComponentRef.forEach(function(filter){
      var targetFilter : Filters =  filter.instance.getFilter();
      if(targetFilter){
        paginate.paginateFilter.filters.push(targetFilter)
      }

    },this);

    if(paginate.paginateFilter.filters.length >0){
    paginate.paginateFilter.clausule = this.conditionFilter.value;
    paginate.paginateFilter.property = paginate.paginateFilter.filters[0].property;
    paginate.paginateFilter.value = paginate.paginateFilter.filters[0].value;
    paginate.paginateFilter.operator = paginate.paginateFilter.filters[0].operator;
    paginate.paginateFilter.filters.splice(0,1);
    }
    else {
      paginate.paginateFilter = null;
    }

    

    this.removeItemFilterDestroy();
    this.paginateEvent.emit(paginate);
    
  
  }
 
}

