import { Routes } from '@angular/router';
import { CustomerList } from './features/customers/customer-list/customer-list';
import { CustomerForm } from './features/customers/customer-form/customer-form';
import { InvoiceList } from './features/invoices/invoice-list';
import { InvoiceForm } from './features/invoices/invoice-form';
import { InvoiceView } from './features/invoices/invoice-view/invoice-view';

export const routes: Routes = [
  { path: '', redirectTo: 'customers', pathMatch: 'full' },
  { path: 'customers', component: CustomerList },
  { path: 'customers/new', component: CustomerForm },
  { path: 'customers/edit/:id', component: CustomerForm },
  
  { path: 'invoices', component: InvoiceList },
  { path: 'invoices/new', component: InvoiceForm },
  { path: 'invoices/:id', component: InvoiceView } 
];

