import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerRequestModel, CustomerResponseModel, UpdateCustomerRequestModel } from '../shared/models/customer-model';
import { InvoiceListResponseModel, InvoiceRequestModel, InvoiceResponseModel } from '../shared/models/invoice-model';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl = 'https://localhost:7084/api';

  constructor(private http: HttpClient) {}

  /** ---------------- CUSTOMERS ---------------- **/

  getAllCustomers(): Observable<CustomerResponseModel[]> {
    return this.http.get<CustomerResponseModel[]>(`${this.baseUrl}/customer`);
  }

  getCustomerById(id: number): Observable<CustomerResponseModel> {
    return this.http.get<CustomerResponseModel>(`${this.baseUrl}/customer/${id}`);
  }

  createCustomer(customer: CustomerRequestModel): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/customer`, customer);
  }

  updateCustomer(customer: UpdateCustomerRequestModel): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/customer/${customer.id}`, customer);
  }

  /** ---------------- INVOICES ---------------- **/

  getAllInvoices(): Observable<InvoiceListResponseModel[]> {
    return this.http.get<InvoiceListResponseModel[]>(`${this.baseUrl}/invoice`);
  }  
  getInvoiceById(id: number): Observable<InvoiceResponseModel> {
    return this.http.get<InvoiceResponseModel>(`${this.baseUrl}/invoice/${id}`);
  }

  createInvoice(invoice: InvoiceRequestModel): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/invoice`, invoice);
  }
}
