import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StorageService } from '../../storage/storage.service';
import { catchError, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../user.service';

interface Assets {
  assetId: number;
  serialNumber: string;
}

@Component({
  selector: 'app-new-request',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, FormsModule],
  templateUrl: './new-request.component.html',
  styleUrl: './new-request.component.css'
})

export class NewRequestComponent implements OnInit{ 

  userDetails: { role: string; employeeId: number } | null = null;
  assets: Assets[] = [];
  storageService = inject(StorageService);
  selectedRequestType: string = 'Replace Asset';
  
  constructor(private route: ActivatedRoute, private http: HttpClient, private userService: UserService) {}

  ngOnInit(): void {
    this.userDetails = this.userService.getUserDetails();
    this.getAssets();
  }

  createRequestForm = new FormGroup({
    requestType: new FormControl<string>('Replace Asset', Validators.required),
    SerialNumber: new FormControl<string|null>(null),
    ReqAssetType: new FormControl<string>(''),
    reason: new FormControl<string>('', Validators.required),
  });

  getAssets(): void {
    const empId = this.userDetails?.employeeId; 
    if (empId) {
      const url = `https://localhost:7032/api/AssetAllocationAPI/AllocationsByEmpId/${empId}`;
      this.http.get<Assets[]>(url)
        .pipe(
          catchError(error => {
            console.error('Error fetching Assets:', error);
            return of([]);
          })
        )
        .subscribe(
          (data: Assets[]) => {
            this.assets = data;
            console.log(this.assets);
          }
        );
    }
  }

  onRequestTypeChange(): void {
    this.selectedRequestType = this.createRequestForm.value.requestType!;
  }

  onSubmit() {
    if (this.createRequestForm.invalid) {
      // Mark form controls as touched to trigger validation messages
      this.createRequestForm.markAllAsTouched();
      alert("Invalid form, fill all the fields correctly.");
      return;
    }

    const requestBody: any = {
      ReqCreatedBy: this.userDetails?.employeeId,
      RequestType: this.selectedRequestType,
      reason: this.createRequestForm.value.reason,
      ReqStatus: "pending",
    };
    console.log(this.createRequestForm.value.SerialNumber);
    console.log(this.assets);
     if (this.selectedRequestType === 'Replace Asset') {
      const selectedSerialNumber = this.createRequestForm.value.SerialNumber;
      const selectedAsset = this.assets.find(asset => asset.serialNumber === selectedSerialNumber);
      console.log(selectedAsset?.assetId);
      if (selectedAsset) {
        requestBody.assetId = selectedAsset.assetId;
      } else {
        console.error('Selected serial number not found in assets list');
        return;
      }
    } else if (this.selectedRequestType === 'New Asset') {
      requestBody.ReqAssetType = this.createRequestForm.value.ReqAssetType;
    }

    console.log(requestBody);

    // Make a POST request to your API endpoint
    this.http.post<any>('https://localhost:7032/api/RequestAPI/Requests', requestBody)
      .subscribe(
        response => {
          console.log('POST request successful!', response);
          alert("Request Sent Successfully.")
          this.createRequestForm.reset();
          // Optionally, handle success response here
        },
        error => {
          console.error('Error making POST request:', error);
          alert("Error making the request.")
          this.createRequestForm.reset();
          // Optionally, handle error response here
        }
      );
  }
}
