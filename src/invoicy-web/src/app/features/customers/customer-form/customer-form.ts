import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../core/api.service';
import { CustomerRequestModel, UpdateCustomerRequestModel, CustomerResponseModel } from '../../../shared/models/customer-model';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-form.html',
  styleUrls: ['./customer-form.scss']
})
export class CustomerForm implements OnInit {
  customerForm: FormGroup;
  customerId?: number;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      telephoneNumber: ['', Validators.required],
      address: this.fb.group({
        province: ['', Validators.required],
        city: ['', Validators.required],
        suburb: [''],
        street: ['', Validators.required],
        zipCode: ['', Validators.required]
      })
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.customerId = Number(idParam);
      this.loadCustomer();
    }
  }

  loadCustomer(): void {
    this.apiService.getCustomerById(this.customerId!).subscribe({
      next: (customer: CustomerResponseModel) => {
        this.customerForm.patchValue({
          name: customer.name,
          telephoneNumber: customer.telephoneNumber,
          address: {
            province: customer.address.province,
            city: customer.address.city,
            suburb: customer.address.suburb,
            street: customer.address.street,
            zipCode: customer.address.zipCode
          }
        });
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to load customer', err);
        this.toastr.error('Failed to load customer');
      }
    });
  }

  onSubmit(): void {
    if (this.customerForm.invalid) {
      this.customerForm.markAllAsTouched();
      return;
    }

    if (this.isEditMode) {
      const updatedCustomer: UpdateCustomerRequestModel = {
        id: this.customerId!,
        ...this.customerForm.value
      };

      this.apiService.updateCustomer(updatedCustomer).subscribe({
        next: () => {
          this.toastr.success('Customer updated successfully', '', { positionClass: 'toast-bottom-center' });
          this.router.navigate(['/customers']);
        },
        error: (err: HttpErrorResponse) => {
          console.error('Error updating customer', err);
          if (err.status === 409) {
            this.toastr.warning('Duplicate telephone number found.', '', { positionClass: 'toast-bottom-center' });
          } else {
            this.toastr.error('Failed to update customer', '', { positionClass: 'toast-bottom-center' });
          }
        }
      });

    } else {
      const newCustomer: CustomerRequestModel = this.customerForm.value;

      this.apiService.createCustomer(newCustomer).subscribe({
        next: () => {
          this.toastr.success('Customer created successfully', '', { positionClass: 'toast-bottom-center' });
          this.router.navigate(['/customers']);
        },
        error: (err: HttpErrorResponse) => {
          console.error('Error creating customer', err);
          if (err.status === 409) {
            this.toastr.warning('Duplicate telephone number found.', '', { positionClass: 'toast-bottom-center' });
          } else {
            this.toastr.error('Failed to create customer', '', { positionClass: 'toast-bottom-center', timeOut: 3000, });
          }
        }
      });
    }
  }
}
