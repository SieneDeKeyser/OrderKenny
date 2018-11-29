import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {Observable, of} from 'rxjs';
import {catchError, map, tap} from 'rxjs/operators';
import { Customer } from '../customers/customer';

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

  private handleError<T> (operation = 'operation', result?:T){
    return (error : any): Observable<T> => {
      
      console.error(error);
      
      console.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    }
  }
}
