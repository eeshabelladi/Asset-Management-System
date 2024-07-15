import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { EmpPageComponent } from "../emp-page/emp-page.component";
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { StorageService } from '../../storage/storage.service';
import { CommonModule } from '@angular/common';
import { SelectRoleComponent } from "../select-role/select-role.component";
import { UserService } from '../../user.service';

@Component({
    selector: 'app-layout',
    standalone: true,
    templateUrl: './layout.component.html',
    styleUrl: './layout.component.css',
    imports: [EmpPageComponent, RouterOutlet, RouterLink, CommonModule, SelectRoleComponent]
})

export class LayoutComponent implements AfterViewInit {
  userDetails: { role: string; employeeId: number } | null = null;
  storageService = inject(StorageService);
  selectedRole: string = 'Employee'; // Default role

  constructor(private router: Router, private userService: UserService) {
    
  }

  ngAfterViewInit(): void {
    this.updateSidebar(this.selectedRole);
  }

  onRoleSelected(role: string): void {
    this.selectedRole = role;
    this.updateSidebar(role);
  }

  updateSidebar(role: string): void {
    console.log(`Sidebar updated for role: ${role}`);
  }

  onLogout() {
    if (confirm('Are you sure you want to logout?')) {
      this.userService.clearUserDetails();
      this.storageService.removeItem('auth');
      sessionStorage.clear();
      this.router.navigate(['/login']);
    }
  }
}

