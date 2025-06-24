import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ApiService } from '../../../core/api.service';
import { CustomerResponseModel } from '../../../shared/models/customer-model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './customer-list.html',
  styleUrls: ['./customer-list.scss']
})
export class CustomerList implements OnInit {
  customers: CustomerResponseModel[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.apiService.getAllCustomers().subscribe({
      next: (data: CustomerResponseModel[]) => {
        this.customers = data;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error loading customers', err);
      }
    });
  }
}
