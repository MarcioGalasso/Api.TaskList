import { OnInit, NgZone } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { NgForm } from "@angular/forms";
import { HttpModule } from '@angular/http';
import 'core-js/es6/object';
import 'core-js/es6/object';
import 'core-js/es6';
import * as Q from "q";
import { MatSnackBar } from "@angular/material";
import { debug } from "util";


export abstract class BaseComponent<TServer, TModelo> implements OnInit {
    public abstract tableName : string;
    public edicao: boolean = false;
    public entidade:TModelo
    public service = null;
    public  zone :  NgZone;
    constructor(
        public _route: ActivatedRoute, 
        public _router: Router,
        public _service: TServer,
        public _entidade:  new () => TModelo,
        public  _ngZone :  NgZone
    ) 
    {        
        this.service = _service;
        this.entidade = new _entidade();
        this.zone = _ngZone;
    }
    
    //Metodo para subscrita -> sera invocado após a criação do component 
    public  initComponent () : Q.Promise<boolean> {
        return Q.resolve(true);
    }
    
    //Metodo para subscrita -> sera invocado após a carregar todos os dados da view
    public configView (){ }
    
    ngOnInit() {  
        this.initComponent().then(function (resolve){
            if(resolve){
                this.entidade = this.service.createNew();      
            }
        }).finally(function(){
            this.configView();
        })
        
    }

    navigateBack(): void{
        this._router.navigate(['/'+this.tableName]);
    }

    onBack(): void {
        this.navigateBack();
    }

    navigateSubmit(): void{
        this._router.navigate(['/']);
    }

    onSubmit(frm : NgForm) { 
        debugger;
        if(!frm.valid) return;
        this.service.save(this.entidade).subscribe(result => {
            this.navigateSubmit();
        },
        error => alert(<any>error));
    }

    remove (obj)  {
        debugger;
        this.service.remove(obj.id).subscribe(result => {
            this.navigateSubmit();
        },
        error => alert(<any>error));
    }
    cancel() {
        this.navigateBack();
    }


}
