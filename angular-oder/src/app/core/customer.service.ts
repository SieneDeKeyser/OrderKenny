import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {Observable, of} from 'rxjs';
import {catchError, map, tap} from 'rxjs/operators';
import { Customer } from '../customers/customer';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  private customersUrl= "http://localhost:57340/api/customers";

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.customersUrl)
                    .pipe(
                      tap(_=> console.log('Getting all costumers')),
                      catchError(this.handleError('getAllCustomers', []))
                    );
  }

  addNewCustomer(customer:Customer): Observable<Customer> {
    delete customer.id;

    return this.http.post<Customer>(this.customersUrl, customer, httpOptions)
        .pipe(
          tap((c:Customer) => console.log(`adding new customer with ${c.id}`)),
          catchError(this.handleError<Customer>('creatingCustomer'))
        );
  }

  getCustomer(id: string): Observable<Customer>{
    const url = `${this.customersUrl}/${id}`;
    return this.http.get<Customer>(url)
                    .pipe(
                      tap(_ => console.log(`getCostumer with id= ${id}`)),
                      catchError(this.handleError<Customer>(`getCostumer ${id}`))
                    );
  }

  private handleError<T> (operation = 'operation', result?:T){
    return (error : any): Observable<T> => {
      
      console.error(error);
      
      console.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    }
  }
}
