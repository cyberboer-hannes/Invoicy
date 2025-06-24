import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from '../../core/api.service';
import { CustomerResponseModel, CustomerRequestModel } from '../../shared/models/customer-model';
import { AddressRequestModel } from '../../shared/models/address-model';
import { InvoiceDetailRequestModel } from '../../shared/models/invoice-detail-model';
import { InvoiceRequestModel } from '../../shared/models/invoice-model';

@Component({
  selector: 'app-invoice-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './invoice-form.html',
  styleUrl: './invoice-form.scss'
})
export class InvoiceForm implements OnInit {

  // Customer fields
  customerName: string = '';
  customerTelephone: string = '';
  customerAddress: AddressRequestModel = {
    province: '',
    city: '',
    suburb: '',
    street: '',
    zipCode: ''
  };

  searchResults: CustomerResponseModel[] = [];

  // Invoice details
  newDetail: InvoiceDetailRequestModel = {
    itemDescription: '',
    quantity: 1,
    unitPrice: 0
  };
  invoiceDetails: InvoiceDetailRequestModel[] = [];

  constructor(
    private apiService: ApiService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  /** CUSTOMER SEARCH **/
  onCustomerSearch(): void {
    if (this.customerName.length < 2 && this.customerTelephone.length < 2) {
      this.searchResults = [];
      return;
    }

    this.apiService.getAllCustomers().subscribe({
      next: (customers) => {
        this.searchResults = customers.filter(c =>
          (this.customerName && c.name.toLowerCase().includes(this.customerName.toLowerCase())) ||
          (this.customerTelephone && c.telephoneNumber.includes(this.customerTelephone))
        );
      },
      error: () => {
        this.toastr.error('Error loading customers.');
      }
    });
  }

  selectCustomer(customer: CustomerResponseModel): void {
    this.customerName = customer.name;
    this.customerTelephone = customer.telephoneNumber;
    this.customerAddress = { ...customer.address };
    this.searchResults = [];
    this.toastr.info('Customer details loaded.');
  }

  /** INVOICE DETAIL MANAGEMENT **/
  addDetail(): void {
    if (!this.newDetail.itemDescription || this.newDetail.quantity <= 0 || this.newDetail.unitPrice <= 0) {
      this.toastr.warning('Please enter valid item details.');
      return;
    }
    this.invoiceDetails.push({ ...this.newDetail });
    this.newDetail = { itemDescription: '', quantity: 1, unitPrice: 0 };
  }

  editDetail(index: number): void {
    this.newDetail = { ...this.invoiceDetails[index] };
    this.invoiceDetails.splice(index, 1);
  }

  removeDetail(index: number): void {
    this.invoiceDetails.splice(index, 1);
  }

  calculateTotal(): number {
    return this.invoiceDetails.reduce((sum, item) => sum + (item.quantity * item.unitPrice), 0);
  }

  /** SAVE INVOICE **/
  saveInvoice(): void {
    if (!this.customerName || !this.customerTelephone || this.invoiceDetails.length === 0) {
      this.toastr.warning('Please fill in customer info and add at least one invoice detail.');
      return;
    }

    const invoice: InvoiceRequestModel = {
      customer: {
        name: this.customerName,
        telephoneNumber: this.customerTelephone,
        address: this.customerAddress
      },
      invoiceDetails: this.invoiceDetails
    };

    this.apiService.createInvoice(invoice).subscribe({
      next: () => {
        this.toastr.success('Invoice saved successfully!');
        this.router.navigate(['/invoices']);
      },
      error: () => {
        this.toastr.error('Error saving invoice.');
      }
    });
  }
}
