import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee.model';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AsyncPipe, CommonModule } from '@angular/common';
import { EmpDetailsComponent } from "./Employee/emp-details/emp-details.component";
import { BrowserModule } from '@angular/platform-browser';
import { SelectRoleComponent } from "./pages/select-role/select-role.component";
import { HeaderComponent } from "./header/header.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, EmpDetailsComponent, CommonModule, HttpClientModule, SelectRoleComponent, HeaderComponent]
})
export class AppComponent {
  title(title: any) {
    throw new Error('Method not implemented.');
  }

}
