import { Component, OnInit } from '@angular/core';
import { Item } from '../item';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ItemsService } from 'src/app/core/items.service';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-item-update',
  templateUrl: './item-update.component.html',
  styleUrls: ['./item-update.component.css']
})
export class ItemUpdateComponent implements OnInit {

  constructor(private itemService: ItemsService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute,) { }

  itemFormUpdate: FormGroup;
  submitted = false;

  ngOnInit() {    
    this.itemFormUpdate = this.formBuilder.group({
      id: [''],
      name:  ['', Validators.required],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
      amountOfStock: ['', [Validators.required, Validators.pattern('^[1-9][0-9]+$')]],
    });
    
    this.getItemOn(this.itemFormUpdate);
  }

  get i(){
    return this.itemFormUpdate.controls;
  }

  isValid(): boolean
  {
    this.submitted = true;
    if(this.itemFormUpdate.invalid)
    {
      return false;
    }

    return true;
  }

  updateItem(item : Item): void {
    this.itemService
        .updateItem(item)
        .subscribe();
  }

  getItemOn(form: FormGroup): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.itemService
        .getItem(id)
        .pipe(map((i:Item) => {delete i.stockUrgency; return i;}))
        .subscribe(item => form.setValue(item));
  }
}
