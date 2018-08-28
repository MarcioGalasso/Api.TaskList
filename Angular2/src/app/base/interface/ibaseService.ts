import 'rxjs/add/Observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IBaseService<TModel,TModelFit> {
    
    get(id: any): Observable<TModel>;

    getAll(): Observable<TModel>

    createNew(): TModel;

    createNewFit(): TModelFit;

    save(entidade : TModel): Observable<TModel>;

    remove(id): Observable<any>;

    query(state: any, expand: string);

    parse(json);
}
