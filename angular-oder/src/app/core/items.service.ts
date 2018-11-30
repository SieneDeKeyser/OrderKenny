import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import {Item} from '../items/item';
import {catchError, map, tap} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  constructor(private http: HttpClient) { }
  private itemsUrl= "http://localhost:57340/api/Items";

  getAllItems(): Observable<Item []>{
    return this.http.get<Item[]>(this.itemsUrl)
                    .pipe(
                      tap(_=>console.log('get all items')),
                      catchError(this.handleError('getitemsfailed', []))
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
