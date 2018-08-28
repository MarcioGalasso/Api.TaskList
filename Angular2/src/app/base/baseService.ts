import 'rxjs/add/Observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { HttpErrorResponse } from "@angular/common/http/src/response";
import { IBaseModel } from './interface/ibaseModel';
import { map } from 'rxjs/operators/map';
import { Observable } from '../../../node_modules/rxjs';

@Injectable()  
export abstract class BaseService<TModel extends IBaseModel,TModelFit extends IBaseModel > 
    {
    
     private urlBase = 'http://localhost:53540'; 
    public tableName :string;
    constructor(protected http: HttpClient,
             public TModel: new() =>TModel,
             public TModelFit : new ()=> TModelFit,
             public tablename :string
             ) { 
                
                 this.tableName = tablename;   
    }

    get(id: any): Observable<any>{ 
        return this.http.get<Observable<any>>(this.urlBase+"/api/" + this.tableName + "/" + id)
                         .catch(this.handleError);
    }

    getAll(): Observable<any>{
        return this.http.get<Observable<any>>(this.urlBase+"/api/" + this.tableName)
                        .catch(this.handleError);
    }

    createNew(): TModel{
        return new this.TModel();
    }

    createNewFit(): TModelFit{
        return new this.TModelFit();
    }

    save(entidade : TModel): Observable<any>{
        if(entidade.id == 0){
            return this.http.post(this.urlBase+"/api/"+ this.tableName, entidade).catch(this.handleError);
        } else {
            return this.http.put(this.urlBase+"/api/"+ this.tableName, entidade).catch(this.handleError);
        }
    }

    remove(id): Observable<any>{
        return this.http.delete(this.urlBase+"/api/" + this.tableName + "/" + id).catch(this.handleError);
    }

    public handleError(err: HttpErrorResponse) {
        if(err.status == 400){
            console.log(err.error);
            return Observable.throw(err.error);
        } else {
            console.log(err.message);
            return Observable.throw(err.message);
        }
    }
}