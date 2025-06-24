import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../../core/api.service';
import { InvoiceResponseModel } from '../../../shared/models/invoice-model';

@Component({
  selector: 'app-invoice-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoice-view.html',
  styleUrls: ['./invoice-view.scss']
})
export class InvoiceView implements OnInit {

  invoice?: InvoiceResponseModel;

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      const id = Number(idParam);
      this.apiService.getInvoiceById(id).subscribe({
        next: (data) => {
          this.invoice = data;
        },
        error: () => {
          alert('Failed to load invoice.');
        }
      });
    }
  }

  /** Calculate Invoice Total **/
  getInvoiceTotal(): number {
    if (!this.invoice) return 0;
    return this.invoice.invoiceDetails.reduce((sum, item) =>
      sum + (item.quantity * item.unitPrice), 0
    );
  }
}
