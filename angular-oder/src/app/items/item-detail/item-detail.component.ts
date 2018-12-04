import { Component, OnInit, Input } from '@angular/core';
import { Item } from '../item';
import { ItemsService } from 'src/app/core/items.service';
import { ActivatedRoute } from '@angular/router';
import {Location} from '@angular/common';


@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {

  item:Item = new Item();

  constructor(private itemService: ItemsService,
              private route: ActivatedRoute,
              private location: Location) { }

  ngOnInit() {
    this.getItem();
  }

  getItem(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.itemService.getItem(id)
                    .subscribe(item => this.item = item);
  }

}
