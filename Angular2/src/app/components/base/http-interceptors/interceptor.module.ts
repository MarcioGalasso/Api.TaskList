import { Injectable, NgModule} from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import 'rxjs/add/operator/do';
import { AuthService } from '../../../services/base/auth/auth.service';
@Injectable()
export class HttpsRequestInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService){}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    

    if (req.url.indexOf('api/token') != -1){
      const dupReq = req.clone({
        url: req.url
      });
      return next.handle(dupReq);
    }
    else{
      const dupReq = req.clone({  
        headers: req.headers.set('token', localStorage.getItem('token') || ''), 
        url: req.url
      });
      return next.handle(dupReq).catch( error => {
        if(error instanceof HttpErrorResponse){
          if(error.status === 401){            
            this.authService.logout();
            return Observable.throw(error);
          }
        }
      }); 
    }
  }
};
@NgModule({
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpsRequestInterceptor, multi: true }
  ]
})
export class InterceptorModule { }
