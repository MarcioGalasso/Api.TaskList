import { Observable } from 'rxjs/Observable';

export interface IPageResult {
    page: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
    previousPage: string;
    nextPage: string;
    data: Observable<any[]>;    
}

export class PageResult implements IPageResult{
    constructor(public page: number,
                public pageSize: number,
                public totalItems: number,
                public totalPages: number,
                public previousPage: string,
                public nextPage: string,
                public data: Observable<any[]>){
    }
}