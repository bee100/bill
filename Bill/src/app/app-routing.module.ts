import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login.component';
import { OverviewComponent } from './overview.component';
import { AuthGuardService as AuthGuard } from '../shared/services/authGuardService';

const routes: Routes = [
  { path: 'account/login', component: LoginComponent },
  { path: '', component: OverviewComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {
}
