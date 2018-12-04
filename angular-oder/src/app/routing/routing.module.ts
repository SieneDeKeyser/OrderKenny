import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes} from '@angular/router';
import { CustomersComponent } from '../customers/customers.component';
import { ItemsComponent } from '../items/items.component';
import { CreateItemComponent } from '../items/create-item/create-item.component';
import { ItemDetailComponent } from '../items/item-detail/item-detail.component';
import { ItemUpdateComponent } from '../items/item-update/item-update.component';

const routes: Routes = [
  {path: '', redirectTo:'/customers', pathMatch:'full'},
  {path:'customers', component:CustomersComponent},
  {path:'items', component: ItemsComponent},
  {path: 'items/newItem', component: CreateItemComponent},
  {path: 'items/:id', component: ItemDetailComponent},
  {path: 'items/update/:id', component: ItemUpdateComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class RoutingModule { }
