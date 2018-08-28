import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { ITask, TaskModelo } from './iTask';
import { BaseComponent } from '../../../base/baseComponent';
import { TaskService } from './task.service';
import { ActivatedRoute, Router } from '../../../../../node_modules/@angular/router';
import { NgForm } from '../../../../../node_modules/@angular/forms';



@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
  providers:[TaskService]
})
export class TaskComponent extends BaseComponent<TaskService,TaskModelo> {
  @ViewChild('paginatorGuest') paginatorTask;
    
  public tableName: string;
  public entidade : ITask = new TaskModelo();  
  public taskList : Array<TaskModelo>;

constructor(public _route: ActivatedRoute, 
            public _router: Router,
            public service: TaskService,
            private ngZone : NgZone) { 
        super(_route,_router,service,TaskModelo,ngZone);
        this.taskList = new Array<TaskModelo>();
      } 
  configView() {
   

  }

  
  onSubmit(frm : NgForm) { 
    debugger;
    if(!frm.valid) return;
    this.service.save(this.entidade).subscribe(result  =>  {
        this.entidade = new TaskModelo();
        this.paginatorTask.resetProperty();
        this.paginatorTask.onPaginate()
    },
    error => alert(<any>error));
}

    private atualizarStatus (entidade :TaskModelo ){
    debugger;
    entidade.status= entidade.status == 0 ? 1 : 0;
      this.service.save(entidade).subscribe(result => {
        this.paginatorTask.updateList(result);
    },
    error => alert(<any>error));

  }

    private removeTask (entidade){
      this.service.remove(entidade.id).subscribe(result =>{
        this.paginatorTask.removeList(entidade);
      })

  }
}
