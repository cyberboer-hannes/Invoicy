import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ApiService } from '../../core/api.service';
import { InvoiceListResponseModel } from '../../shared/models/invoice-model';  // ✅ Use list model!

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './invoice-list.html',
  styleUrl: './invoice-list.scss'
})
export class InvoiceList implements OnInit {
  invoices: InvoiceListResponseModel[] = [];
  filteredInvoices: InvoiceListResponseModel[] = [];
  searchQuery: string = '';

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getAllInvoices().subscribe({
      next: (data: InvoiceListResponseModel[]) => {   // ✅ Cast correctly here
        this.invoices = data;
        this.filteredInvoices = data;
      },
      error: (err: any) => {
        console.error('Error loading invoices', err);
      }
    });
  }

  onSearch() {
    const query = this.searchQuery.toLowerCase();
    this.filteredInvoices = this.invoices.filter(invoice =>
      invoice.invoiceNumber.toLowerCase().includes(query) ||
      invoice.customer.name.toLowerCase().includes(query)
    );
  }
}
