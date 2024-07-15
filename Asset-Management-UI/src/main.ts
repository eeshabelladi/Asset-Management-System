import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { LoginPageComponent } from './app/pages/login-page/login-page.component';
import { AppComponent } from './app/app.component';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
