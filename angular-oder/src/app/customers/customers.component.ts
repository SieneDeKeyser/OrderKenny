import { Component, OnInit } from '@angular/core';
import { Customer } from './customer';
import { CustomerService } from '../core/customer.service';
import { FormControl, FormBuilder, FormGroup, Validators  } from '@angular/forms';


@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})



export class CustomersComponent implements OnInit {

  customers: Customer[] = [];
  customerForm: FormGroup;
  submitted = false;
  customer:Customer = new Customer();
  constructor(private customerService: CustomerService, private formBuilder:FormBuilder) {}

  ngOnInit() {
    this.getCustomers();
    this.customerForm = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      streetname: ['', Validators.required],
      streetnumber: ['', Validators.required],
      country: ['', Validators.required],
      postalcode: ['', Validators.required],
      phonenumber: ['', Validators.required],

    });
  }

  addNewCustomer() {
      this.customer.phoneNumber.countryCallingCode ='0032';
      this.customer.email.domain = this.customer.email.complete.split('@')[1];
      this.customer.email.localPart = this.customer.email.complete.split('@')[0];
      this.customerService.addNewCustomer(this.customer)
                          .subscribe(customer => {this.customers.push(customer)});
  }

  get f(){
    return this.customerForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    if(this.customerForm.invalid)
    {
      return;
    }
    this.addNewCustomer();
  }

  getCustomers(): void {
    this.customerService.getAllCustomers()
                        .subscribe(custs => this.customers = custs);
  }

}
