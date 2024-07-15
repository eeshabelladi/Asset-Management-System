import { Component, OnInit, inject } from '@angular/core';
import { StorageService } from '../../storage/storage.service';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { catchError, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { UserService } from '../../user.service';

interface AssetProperties {
  AllocationId: number;
  EmployeeId: number;
  AssetId: number;
  serialNumber: string;
  InventoryId: number;
  assetType: string;
  brand: string;
  model: string;
}

@Component({
  selector: 'app-asset-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './asset-page.component.html',
  styleUrl: './asset-page.component.css'
})

export class AssetPageComponent implements OnInit {

  userDetails: { role: string; employeeId: number } | null = null;
  allocations: AssetProperties[] = [];
  userService = inject(UserService);

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.userDetails = this.userService.getUserDetails();
    this.getAllocations();
  }

  getAllocations(): void {
    const empId = this.userDetails?.employeeId;
    if (empId) {
      const url = `https://localhost:7032/api/AssetAllocationAPI/AllocationsByEmpId/${empId}`;
      this.http.get<AssetProperties[]>(url)
        .pipe(
          catchError(error => {
            console.error('Error fetching allocations:', error);
            return of([]);
          })
        )
        .subscribe(
          (data: AssetProperties[]) => {
            this.allocations = data;
            console.log(this.allocations);
          }
        );
    }
  }
}