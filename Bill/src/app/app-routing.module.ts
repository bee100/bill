import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login.component';
import { OverviewComponent } from './overview.component';
import { AuthGuardService as AuthGuard } from '../shared/services/authGuardService';
import { MainLayoutComponent } from './main.layout.component';

const routes: Routes = [
  
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: OverviewComponent, pathMatch: 'full' }
    ]
  },

  //no layout routes
  {
    path: 'account/login',
    component: LoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {
}
