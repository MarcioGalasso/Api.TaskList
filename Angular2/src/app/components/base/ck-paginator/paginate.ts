

export class Paginate 
{
    public page : number =1;
    public size : number;
    public paginateFilter : PaginateFilter= new PaginateFilter();
    
}


export class PaginateFilter 
{
    public  clausule :string;
    public  filters :Array<Filters> = new Array<Filters>();
    public property : string;
    public value :object;
    public operator :string;
}


export class Filters
{
    public property : string;
    public value :object;
    public operator :string;
}


