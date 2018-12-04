import { Component, OnInit } from '@angular/core';
import { ItemsService } from 'src/app/core/items.service';
import { Item } from '../item';
import { FormControl, FormBuilder, FormGroup, Validators  } from '@angular/forms';



@Component({
  selector: 'app-create-item',
  templateUrl: './create-item.component.html',
  styleUrls: ['./create-item.component.css']
})
export class CreateItemComponent implements OnInit {
  
  item: Item = new Item();
  itemForm:FormGroup;
  submitted = false;

  constructor(private itemService: ItemsService, private formBuilder:FormBuilder) { }

  ngOnInit() {
    this.item.name = 'New item';
    this.itemForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
      amount: ['', [Validators.required, Validators.pattern('^[1-9]+$')]]
  });
  }

  get i(){
    return this.itemForm.controls;
  }

  isValid(): boolean{
    this.submitted=true;
    if(this.itemForm.invalid)
    {
      return false;;
    }
   return true;
  }


  saveNewItem(): void{
    this.itemService.addNewItem(this.item)
                    .subscribe();
  }

}
