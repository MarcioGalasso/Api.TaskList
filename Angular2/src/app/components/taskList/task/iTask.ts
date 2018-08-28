
export interface ITask {
    id: number;
    descricao: string;
    status: number;
    createDateTime: Date;
    
}

export class TaskModelo implements ITask{
   
    constructor (public id : number = 0,
                 public descricao :string = null,
                 public status: number = 0,
                 public createDateTime : Date = new Date()) {   }
    
 }
