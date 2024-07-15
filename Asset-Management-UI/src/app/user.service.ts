import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userDetails: { role: string; employeeId: number } | null = null;
  private credentials: string | null = null;

  setUserDetails(details: { role: string; employeeId: number }): void {
    this.userDetails = details;
  }

  getUserDetails(): { role: string; employeeId: number } | null {
    return this.userDetails;
  }

  clearUserDetails(): void {
    this.userDetails = null;
  }

  setCredentials(username: string, password: string): void {
    this.credentials = btoa(`${username}:${password}`);
  }

  getCredentials(): string | null {
    return this.credentials;
  }

  clearCredentials(): void {
    this.credentials = null;
  }

}
