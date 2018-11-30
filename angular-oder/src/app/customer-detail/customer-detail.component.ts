import { Component, OnInit, Input } from '@angular/core';
import { Customer } from '../customers/customer';
import { CustomerService } from '../core/customer.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent {
  
  @Input() customer: Customer;

}
