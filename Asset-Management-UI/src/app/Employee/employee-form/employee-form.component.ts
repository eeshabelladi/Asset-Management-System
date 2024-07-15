import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Employee } from '../../../models/employee.model';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { EmpDetailsComponent } from '../emp-details/emp-details.component';


@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css'
})
export class EmployeeFormComponent {
 
}
