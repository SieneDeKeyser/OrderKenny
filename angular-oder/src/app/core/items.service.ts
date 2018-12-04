import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Item} from '../items/item';
import {catchError, map, tap} from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

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

  addNewItem(item: Item): Observable<Item>{
    delete item.id;

    return this.http.post<Item>(this.itemsUrl, item, httpOptions)
                    .pipe(
                      tap((i:Item) => console.log(`Creating new item with ${i.id}`)),
                      catchError(this.handleError<Item>(`CreatingNewItem`))
                    );
  }

  getItem(id: string): Observable<Item> {
    const url = `${this.itemsUrl}/${id}`;
    return this.http.get<Item>(url)
                    .pipe(
                      tap((i:Item)=> console.log(`get one item with ${id} ${i.name}`)),
                      catchError(this.handleError(`getItem id = ${id}`, new Item()))
                    );
  }

  updateItem(item: Item): Observable<Item> {
    const url = `${this.itemsUrl}/${item.id}`;
    return this.http.put<Item>(url, item, httpOptions)
                    .pipe(
                      tap((i: Item) => console.log(`updating item ${i.id}`)),
                      catchError(this.handleError<Item>(`updating item ${item.id}`))
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
