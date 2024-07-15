import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { loggerInterceptor } from '../../interceptor/logger.interceptor';
import { StorageService } from '../../storage/storage.service';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})

export class LoginPageComponent {
  loading = false;
  http = inject(HttpClient);
  storageService = inject(StorageService);
  userService = inject(UserService);
  
  constructor(private router: Router,) {
    sessionStorage.setItem("isLoggedIn","false");
  }
  
  loginForm = new FormGroup({
    gid: new FormControl<string>('', Validators.required),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(4)]),
  });
  
  private encodeCredentials(gid: string, password: string): string {
    return btoa(`${gid}:${password}`);
  }
  
  onLogin() {
    const gid = this.loginForm.get('gid')?.value;
    const password = this.loginForm.get('password')?.value;
    
    if (gid && password) {
      this.userService.setCredentials(gid, password);
      const encodedCredentials = this.encodeCredentials(gid, password);
      this.storageService.setItem('auth', encodedCredentials, 600000);
      
      // Now, send a GET request to your login API
      this.http.get<{ role: string, employeeId: number }>('https://localhost:7032/Employees/Login')
      .subscribe(
        (res) => {
          const { role, employeeId: id } = res;
          this.userService.setUserDetails({ role, employeeId: id });
          sessionStorage.setItem("isLoggedIn", "true");
          this.router.navigate(['/dashboard']);
        },
        (error) => {
          console.error('Login failed', error);
          sessionStorage.setItem("isLoggedIn", "false");
          this.loginForm.reset();
        }
      );
    }
  }
  
  onReset() {
    this.loginForm.reset();
  }
  
}
