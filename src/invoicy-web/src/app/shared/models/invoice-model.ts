import { CustomerRequestModel } from './customer-model';
import { CustomerResponseModel } from './customer-model';
import { InvoiceDetailRequestModel } from './invoice-detail-model';
import { InvoiceDetailResponseModel } from './invoice-detail-model';

export interface InvoiceResponseModel {
  id: number;
  invoiceNumber: string;
  invoiceDate: string;
  customer: CustomerResponseModel;
  invoiceDetails: InvoiceDetailResponseModel[];
}

export interface InvoiceRequestModel {
  customer: CustomerRequestModel;
  invoiceDetails: InvoiceDetailRequestModel[];
}

export interface InvoiceListResponseModel {
  id: number;
  invoiceNumber: string;
  invoiceDate: string;
  customer: CustomerResponseModel;
}