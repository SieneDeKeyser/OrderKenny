import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RoutingModule } from './routing/routing.module';


import { AppComponent } from './app.component';
import { CustomersComponent } from './customers/customers.component';
import { CoreModule } from './core/core.module';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { ItemsComponent } from './items/items.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomersComponent,
    CustomerDetailComponent,
    ItemsComponent
  ],
  imports: [
    BrowserModule,
    CoreModule,
    RoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
