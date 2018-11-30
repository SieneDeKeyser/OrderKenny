import { Component, OnInit } from '@angular/core';
import { Item } from './item';
import { ItemsService } from '../core/items.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {

  constructor(private itemService:ItemsService) { }
  items: Item [] = [];

  ngOnInit() {
    this.getItems();
  }

  getItems():void {
    this.itemService.getAllItems()
                    .subscribe(allItems=>this.items=allItems);
  }

}
