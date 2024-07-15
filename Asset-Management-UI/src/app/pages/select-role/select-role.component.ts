import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { StorageService } from '../../storage/storage.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-select-role',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './select-role.component.html',
  styleUrls: ['./select-role.component.css']
})
export class SelectRoleComponent implements OnInit {

  userDetails: { role: string; employeeId: number } | null = null;
  storageService = inject(StorageService);
  defaultRole: string = 'Employee'; // Default selected role
  roles: string[] = [this.defaultRole]; // Start with default role
  selectedRole: string = this.defaultRole; // Initialize selected role
  showRoleDropdown = false;
  showSelectRoleButton = false;

  @Output() roleSelected: EventEmitter<string> = new EventEmitter<string>();

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.userDetails = this.userService.getUserDetails();
    const storedRole = this.userDetails?.role;
    console.log(storedRole);
    this.selectedRole = this.defaultRole; // Set selected role from storage or default

    // Determine roles to display
    this.roles = [this.defaultRole]; // Always include 'Employee' as default

    // Add additional roles based on user's role
    if (storedRole === 'Admin' || storedRole === 'Manager' || storedRole === 'IT infrastructure User') {
      this.roles.push(storedRole); // Add specific role if applicable
    }

    this.showSelectRoleButton = true; // Show select role button
  }

  toggleRoleDropdown(event: MouseEvent): void {
    event.preventDefault(); // Prevent default link behavior
    this.showRoleDropdown = !this.showRoleDropdown; // Toggle dropdown visibility
  }

  selectRole(role: string): void {
    console.log(`Selected role: ${role}`); // Update selected role
    this.selectedRole = role;
    this.showRoleDropdown = false; // Hide dropdown
    this.storageService.setItem('role', role, 10000); // Store selected role for next session
    this.roleSelected.emit(role);
  }
}
