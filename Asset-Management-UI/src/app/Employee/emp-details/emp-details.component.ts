
import { Component, inject } from '@angular/core';
import { Observable, catchError, map, of, startWith } from 'rxjs';
import { Employee } from '../../../models/employee.model';
import { HttpClient } from '@angular/common/http';
import { AsyncPipe, CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';


interface UpdateEmployeeRequest {
  id: string | null;
  gid: string;
  fullName: string;
  email: string;
  password: string;
  isActive: boolean;
  managerId: string | null;
  empCreatedBy: string;
}


@Component({
  selector: 'app-emp-details',
  standalone: true,
  imports: [ AsyncPipe, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './emp-details.component.html',
  styleUrls: ['./emp-details.component.css']
})

export class EmpDetailsComponent {
  private http = inject(HttpClient);

  createEmployeeForm = new FormGroup({
    gid: new FormControl<string>('', Validators.required),
    fullName: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', Validators.required),
    managerId: new FormControl<string | null>(null, Validators.required)
  });

  editEmployeeForm = new FormGroup({
    id: new FormControl<string | null>(null),
    gid: new FormControl<string>({ value: '', disabled: true }, Validators.required),
    fullName: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    isActive: new FormControl<string>('true', Validators.required),
    managerId: new FormControl<string | null>(null, Validators.required),
  });

  employees$: Observable<Employee[]> = this.getEmployees();
  isEditMode = false;
  searchGid: string = '';
  originalGid: string = '';
originalPassword: string = '';
originalEmpCreatedBy: string = '';


  // Method to handle form submission for creating a new employee
  onCreateFormSubmit() {
    if (this.createEmployeeForm.invalid) {
      alert("Fill out the form correctly.");
      return;
    }

    const addEmployeeRequest = {
      gid: this.createEmployeeForm.value.gid!,
      fullName: this.createEmployeeForm.value.fullName!,
      email: this.createEmployeeForm.value.email!,
      password: this.createEmployeeForm.value.password!,
      isActive: true,
      managerId: this.createEmployeeForm.value.managerId!,
      empCreatedBy: 7,
    };
    console.log(addEmployeeRequest);

    this.http.post('https://localhost:7032/Employees', addEmployeeRequest)
      .subscribe({
        next: () => {
          this.employees$ = this.getEmployees();
          this.createEmployeeForm.reset();
          window.alert("Employee Created!");
        },
        error: (err) => console.error('Failed to create employee:', err)
      });
  }

  // Method to handle form submission for editing an existing employee
  onEditFormSubmit() {
    if (this.editEmployeeForm.invalid) {
      alert("Fill out the form correctly.");
      return;
    }

    const updateEmployeeRequest: UpdateEmployeeRequest = {
      id: this.editEmployeeForm.value.id!,
      gid: this.originalGid, 
      fullName: this.editEmployeeForm.value.fullName!,
      email: this.editEmployeeForm.value.email!,
      password: this.originalPassword, 
      isActive: this.editEmployeeForm.value.isActive === 'true',
      managerId: this.editEmployeeForm.value.managerId!,
      empCreatedBy: this.originalEmpCreatedBy
    };
    console.log(updateEmployeeRequest);
    this.http.put('https://localhost:7032/Employees/' + updateEmployeeRequest.id, updateEmployeeRequest)
    .subscribe({
      next: () => {
        this.employees$ = this.employees$.pipe(
          map(employees => employees.map(emp => emp.employeeId === updateEmployeeRequest.id ? {
            ...emp,
            ...updateEmployeeRequest,
            isActive: updateEmployeeRequest.isActive,
            managerId: updateEmployeeRequest.managerId
          } : emp))
        );
        this.editEmployeeForm.reset();
        this.isEditMode = false;
        window.alert("Employee Updated!");
      },
      error: (err) => console.error('Failed to update employee:', err)
    });
}
  
  

  // Method to delete an employee by ID
  onDelete(id: string) {
    const confirmDelete = window.confirm('Are you sure you want to delete this user?');

    if (confirmDelete) {
    this.http.delete('https://localhost:7032/Employees/' + id)
      .subscribe({
        next: () => {
          // Remove the deleted employee from the observable array
          this.employees$ = this.employees$.pipe(
            map(employees => employees.filter(employee => employee.employeeId !== id))
          );
          window.alert("Employee Deleted!");
        },
        error: (err) => console.error('Failed to delete employee:', err)
      });
    }
  }
  

  // Method to get the list of employees
  private getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>('https://localhost:7032/Employees').pipe(
      startWith([]), // Ensures the observable is initialized with an empty array
      catchError(error => {
        console.error('Error occurred while fetching employees:', error);
        return of([]); // Returns an empty array in case of error
      })
    );
  }
  
  

  // Method to load employee details for editing
  loadEmployeeForEdit(gid: string) {
    this.isEditMode = true;
    this.http.get<Employee>('https://localhost:7032/EmployeeByGid/' + gid)
      .subscribe({
        next: (employee) => {
          // Store the original values
          this.originalGid = employee.gid!;
          this.originalPassword = employee.password!;
          this.originalEmpCreatedBy = employee.empCreatedBy!;
  
          this.editEmployeeForm.setValue({
            id: employee.employeeId || null,
            gid: employee.gid || '',
            fullName: employee.fullName || '',
            email: employee.email || '',
            isActive: employee.isActive ? 'true' : 'false',
            managerId: employee.managerId || null
          });
        },
        error: (err) => console.error('Failed to load employee:', err)
      });
  }
  
  

  onSearch() {
    if (!this.searchGid) {
      this.employees$ = this.getEmployees();
      return;
    }
    const url = `https://localhost:7032/EmployeeByGid/${this.searchGid}`;
    this.http.get<Employee>(url)
      .pipe(
        map(employee => [employee]),
        catchError(error => {
          console.error('Error occurred while searching for employee:', error);
          return of([]);
        })
      )
      .subscribe({
        next: (employees) => {
          this.employees$ = of(employees);
        }
      });
  }

}
