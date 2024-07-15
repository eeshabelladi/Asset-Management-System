import { Routes } from '@angular/router';
import { AssetDetailsComponent } from './asset-details/asset-details.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EmpDetailsComponent } from './Employee/emp-details/emp-details.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { Component } from '@angular/core';
import { EmpPageComponent } from './pages/emp-page/emp-page.component';
import { authGuard } from './Guard/auth.guard';

export const routes: Routes = [
   {
      path: 'login',
      component: LoginPageComponent,
   },
  
   { path: '', redirectTo: '/login', pathMatch: 'full' },

   {
    path: '',
    loadComponent: () => import('./pages/layout/layout.component').then((c)=>c.LayoutComponent),
    canActivate:[authGuard],
    children:[
      {
      path: 'dashboard',
      loadComponent: () => import('./pages/emp-page/emp-page.component').then((c)=>c.EmpPageComponent),
      canActivate:[authGuard],
      },
      {
         path: 'View-Employees',
         loadComponent: () => import('./Employee/emp-details/emp-details.component').then((c)=>c.EmpDetailsComponent),
         canActivate:[authGuard],
      },
      {
         path: 'Your-assets',
         loadComponent: () => import('./pages/asset-page/asset-page.component').then((c)=>c.AssetPageComponent),
         canActivate:[authGuard],
      },
      {
         path: 'New-Request',
         loadComponent: () => import('./Requests/new-request/new-request.component').then((c)=>c.NewRequestComponent),
      }

    ]
 },

];
