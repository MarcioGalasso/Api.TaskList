import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './services/base/auth/auth.guard';
import { HomeComponent } from './components/base/home/home.component';
import { TaskComponent } from './components/taskList/task/task.component';
import { LoginComponent } from './components/taskList/login/login.component';

const routes: Routes = [
   { path: '', component: HomeComponent, canActivate: [AuthGuard],
      children : [
            { path: '', component: TaskComponent },
            
      ]
  },
  { path: 'login', component: LoginComponent},
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes),],
    exports: [RouterModule]
})
export class AppRoutingModule { }
