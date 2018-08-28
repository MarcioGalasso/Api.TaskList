import { Input } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Paginate } from "./paginate";
import { Observable } from "rxjs";
import { IBaseModel } from "../../../base/interface/ibaseModel";



export abstract class BasePaginatorListComponent
{
    private urlBase = 'http://localhost:53540';
    private cacheList :  Array<IBaseModel>;
    public abstract record : number;
    private http: HttpClient;
    public paginate : Paginate = new Paginate()
    @Input() public abstract  controller:string;
    @Input() public abstract size: number;
    @Input() public listRegister : Array<IBaseModel>;
    @Input() public abstract useCache : boolean;

    constructor(http: HttpClient)
    {
        this.http = http;
        this.cacheList = new Array<IBaseModel>();
    }

    public paginator ($event) {
        this.paginate.page =$event.pageIndex+1;
        this.onPaginate();
    }

    // private loadPaginate(): Observable<ResultPaginate>{ 
    //     return this.http.get<ResultPaginate>("api/" + this.controller + "/LoadPaginate?page="+this.paginate.page+"&size="+this.paginate.size);
                         
    // }

    private loadPaginate(): Observable<ResultPaginate>{ 
        debugger;
        return this.http.post<ResultPaginate>(this.urlBase+"/api/" + this.controller + "/LoadPaginate",this.paginate);
                         
    }
   
    public resetProperty () {
        this.cacheList = new Array<IBaseModel>();
        this.record = 0;

    }

    public removeList(obj: IBaseModel) {
        var target = this.listRegister.filter(element=> element.id == obj.id);
        this.listRegister.splice(this.listRegister.indexOf(target[0]),1);
        this.removeCache(obj);
        
    }
  
    public updateList(obj: IBaseModel){
        var target = this.listRegister.filter(element=> element.id == obj.id);
        this.listRegister.splice(this.listRegister.indexOf(target[0]),1, obj);
        this.updateCache(obj);
    }

    public addList (obj: IBaseModel){        
        this.listRegister.unshift(obj);
        this.updateCache(obj);
    }

    private removeCache (obj:IBaseModel){
        let lastRegister : number = (this.paginate.page*this.paginate.size);
        if(this.useCache){
            if(lastRegister > this.cacheList.length){
                this.cacheList.splice(lastRegister-this.paginate.size, lastRegister);
            }
            else{
            var target=  this.cacheList.filter(element=> element == obj);
            this.cacheList.splice(this.cacheList.indexOf(target[0]),1);
            this.record -= 1; 
            }
        }
        this.onPaginate();
    }

    private addCache (obj:IBaseModel){
        if(this.useCache){
        this.cacheList.unshift(obj);
        }
    }

    private updateCache (obj:IBaseModel){
        if(this.useCache){
        var target = this.cacheList.filter(element=> element.id == obj.id);
        this.cacheList.splice(this.cacheList.indexOf(target[0]),1, obj);
        }
    }

    private setCache (register : ResultPaginate,lastRegister : number){
        
        register.results.forEach(element=> {
            this.cacheList.push(element)
        });
    
         this.record = register.__count;
    }

    private cleanRegister() {
        this.listRegister.splice(0,this.listRegister.length);
    }

    private setRegister(register:ResultPaginate = null){
        let lastRegister : number = (this.paginate.page*this.paginate.size);
        this.cleanRegister();

        if(this.useCache &&  register!= null && (lastRegister > this.cacheList.length )){  
           this.setCache(register,this.paginate.page);
        }
        
        if(this.useCache){
            var target  = this.cacheList.slice(lastRegister-this.paginate.size, lastRegister);
            target.forEach(element => {
                     this.listRegister.push(element);
                 });
        }
        else{
            register.results.forEach(element => {
                     this.listRegister.push(element);
                 });
            this.record = register.__count;
        }
    }

    public onPaginate(){
        let firstRegister : number = (this.paginate.page*this.paginate.size) - this.paginate.size;
        if(firstRegister >= this.cacheList.length || !this.useCache)
        {
            this.loadPaginate().subscribe(result  =>{
              this.setRegister(result);
            })  
        }
        else{
            this.setRegister();
        }
    }
}


export abstract class ResultPaginate
{
    public __count : number;
    public results : Array<IBaseModel>;
}
