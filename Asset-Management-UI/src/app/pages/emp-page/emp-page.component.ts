import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Employee } from '../../../models/employee.model';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { StorageService } from '../../storage/storage.service';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-emp-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './emp-page.component.html',
  styleUrl: './emp-page.component.css'
})
export class EmpPageComponent implements OnInit {

  userDetails: { role: string; employeeId: number } | null = null;

  employeeDetails: Employee | null = null;
  storageService = inject(StorageService);
  userService = inject(UserService);

  constructor(private route: ActivatedRoute, private http: HttpClient) { }
   
  ngOnInit() {
    this.userDetails = this.userService.getUserDetails();
    this.loadEmployeeDetails();
  }

  loadEmployeeDetails() {

    if (this.userDetails?.employeeId) {
      const url = `https://localhost:7032/Employees/${this.userDetails.employeeId}`;
      this.http.get<Employee>(url)
        .pipe(
          map(employee => employee),
          catchError(error => {
            console.error('Error occurred while fetching employee details:', error);
            return of(null);
          })
        )
        .subscribe({
          next: (employee) => {
            this.employeeDetails = employee;
          }
        });
    }
  }
  }