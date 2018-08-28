import 'rxjs/add/Observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import { Injectable } from "@angular/core";
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { HttpErrorResponse } from "@angular/common/http/src/response";

import { BaseService } from '../../../base/baseService';
import { IBaseService } from '../../../base/interface/ibaseService';

import { TaskModelo } from './iTask';


  
@Injectable()
export class TaskService extends BaseService<TaskModelo,TaskModelo>  {

    constructor(http: HttpClient) 
    {
         super(http,TaskModelo,TaskModelo,'Task');
         
    }

}