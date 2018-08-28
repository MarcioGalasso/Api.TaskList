
import { BrowserModule } from '@angular/platform-browser';

import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AuthGuard } from './services/base/auth/auth.guard';
import { AppComponent } from './app.component';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app.routing.module';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatRadioModule, MatSelectModule, MatInputModule,MatToolbarModule,MatIconModule, MatCheckboxModule, MatFormFieldModule, MatButtonModule, MatChipsModule, MatSnackBarModule } from '@angular/material';
import {MatMenuModule} from '@angular/material/menu';
import { HomeComponent } from './components/base/home/home.component';
import { HeaderComponent } from './components/base/header/header.component';

// Observable class extensions
import 'rxjs/add/observable/of';

// Observable operators
import 'rxjs/add/operator/take';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { TaskComponent } from './components/taskList/task/task.component';
import { AuthService } from './services/base/auth/auth.service';
import { LoginComponent } from './components/taskList/login/login.component';
import { CommonModule } from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { CkPaginatorModule } from './components/base/ck-paginator/ck-paginator.module';
import { HttpsRequestInterceptor, InterceptorModule } from './components/base/http-interceptors/interceptor.module';
import {MatCardModule} from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    TaskComponent,
    LoginComponent
  ],
  imports: [
   AppRoutingModule,
   CommonModule,
   BrowserModule,
   BrowserAnimationsModule,
   AppRoutingModule,
   ReactiveFormsModule,
   HttpModule,
   HttpClientModule,
   MatButtonToggleModule,
   MatButtonModule, 
   FormsModule,
   MatInputModule, 
   MatCheckboxModule,
   MatSelectModule,
   MatFormFieldModule,
   MatRadioModule,
   MatMenuModule, 
   MatToolbarModule,
   MatIconModule,
   HttpModule ,
   MatChipsModule,
   MatSnackBarModule,
   CkPaginatorModule,
   InterceptorModule,
   MatCardModule,
   MatDividerModule

  ],
  providers: [AuthGuard, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule {  }
