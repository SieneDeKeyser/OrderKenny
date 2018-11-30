import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes} from '@angular/router';
import { CustomersComponent } from '../customers/customers.component';
import { ItemsComponent } from '../items/items.component';

const routes: Routes = [
  {path: '', redirectTo:'/customers', pathMatch:'full'},
  {path:'customers', component:CustomersComponent},
  {path:'items', component: ItemsComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class RoutingModule { }
