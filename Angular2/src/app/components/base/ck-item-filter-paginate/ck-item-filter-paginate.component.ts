import { Component, OnInit, Input,
  ViewChild,
  ViewContainerRef,
  ComponentFactoryResolver,
  ComponentRef,
  ComponentFactory  } from '@angular/core';
import { CkItemTextPaginateComponent, CkItemNumberPaginateComponent, CkItemSelectBoxPaginateComponent, CkItemDatePaginateComponent } from './ck-item-value-filter';
import { Filter } from '../ck-paginator/ck-paginator.component';
import { Filters } from '../ck-paginator/paginate';


@Component({
  selector: 'app-ck-item-filter-paginate',
  templateUrl: './ck-item-filter-paginate.component.html',
  styleUrls: ['./ck-item-filter-paginate.component.css']
})
export class CkItemFilterPaginateComponent implements OnInit {
  @ViewChild('itemValue', { read: ViewContainerRef }) entry: ViewContainerRef;

  public clausuleList : Array<Clausule> = new  Array<Clausule>() ;
  public destroy: boolean = false;
  private clausule : Clausule;
  private propertyName : Filter;
  private  componentRef: any;
  public  valueFilter : any = { value: undefined};
  @Input() propertySearch : Array<Filter>;


  constructor(private resolver: ComponentFactoryResolver) { 
  }

  private getClausules(itemSelected) {
    this.clausuleList = [];
    if(!itemSelected.condition){
    this.clausuleList.push(new Clausule("=="));
    this.clausuleList.push(new Clausule(">"));
    this.clausuleList.push(new Clausule(">="));
    this.clausuleList.push(new Clausule("<"));
    this.clausuleList.push(new Clausule("<="));
    console.log(this.clausuleList);
    }
    else{
      itemSelected.condition.split(',').forEach(element => {
        this.clausuleList.push(new Clausule(element));
      });
    }
    this.clausule = this.clausuleList[0];
  }

  ngOnInit() {
    
  }


  public getFilter () : Filters {
    if( !this.destroy  && this.clausule && this.clausule.value 
       && this.propertyName && this.propertyName.column 
       && this.valueFilter && this.valueFilter.value != undefined)
    {
      var retorno : Filters = new Filters();
      retorno.operator = this.clausule.value;
      retorno.property = this.propertyName.column;
      retorno.value = this.valueFilter.value;
      return retorno;
    }
    else {
      this.destroy = true;
      return null;
    }
  }

  public createValueComponente(item){
    this.getClausules(item);
    this.entry.clear();
    switch(item.type){
      case 'text':
      this.createTypeText();
      break;
      case 'number':
      this.createTypeNumber();
      break;
      case 'date':
      this.createTypeDate();
      break;
      case 'selectBox':
      this.createTypeSelect(item.optionList);
      break;
      default: this.createTypeText()
      break;
    }
  }

 

  private createTypeNumber () {
    const factory = this.resolver.resolveComponentFactory(CkItemNumberPaginateComponent);
    const componentRef = this.entry.createComponent(factory);
    componentRef.instance.valueFilter = this.valueFilter;
  }

  private createTypeSelect (optionList) {
    const factory = this.resolver.resolveComponentFactory(CkItemSelectBoxPaginateComponent);
    const componentRef = this.entry.createComponent(factory);
    componentRef.instance.optionList = optionList;
    componentRef.instance.valueFilter = this.valueFilter;
  }

  private createTypeText () {
    const factory = this.resolver.resolveComponentFactory(CkItemTextPaginateComponent);
    const componentRef = this.entry.createComponent(factory);
    componentRef.instance.valueFilter = this.valueFilter;

  }
  
  private createTypeDate () {
    const factory = this.resolver.resolveComponentFactory(CkItemDatePaginateComponent);
    const componentRef = this.entry.createComponent(factory);
    componentRef.instance.valueFilter = this.valueFilter;

  }

  public afterSelectClausule(item){
    console.log(item);
  }

  destroyComponent() {
    this.destroy = true;
  }

}

export class Clausule {
   public value : string;
   public title : string;
   constructor(value : string){
      this.value = value;
      switch(value){
        case "==":
        this.title = "Igual"
        break;
        case ">":
        this.title = "Maior"
        break;
        case ">=":
        this.title = "Maior Igual"
        break;
        case "<":
        this.title = "Menor"
        break;
        case "<=":
        this.title = "Menor Igual"
        break;
      }
   }
}
