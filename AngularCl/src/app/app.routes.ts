import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { Page1Component } from './components/page1/page1.component';
import { Page2Component } from './components/page2/page2.component';
import { Page3Component } from './components/page3/page3.component';

export const routes: Routes = [
    {path: '', component: DashboardComponent, canActivate: [AuthGuard], children: [
        {path: '', redirectTo: 'page1', pathMatch: 'full'},
        {path: 'page1', component: Page1Component},
        {path: 'page2', component: Page2Component},
        {path: 'page3', component: Page3Component},
    ]},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
];
