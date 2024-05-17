import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  // { path: '', data:{breadcrumb:'Account'} },
  { path: 'login', component: LoginComponent, data:{breadcrumb:'Login'} },
  { path: 'register', component: RegisterComponent, data:{breadcrumb:'Sign Up'} }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AccountRouitngModule { }
