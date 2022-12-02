import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TabsComponent } from './components/tabs/tabs.component';
import { TransactionComponent } from './components/transaction/transaction.component';
import { ChartsComponent } from './components/charts/charts.component';
import { NgChartsModule } from 'ng2-charts';
import { HttpClientModule } from '@angular/common/http';
import { TransactionsListComponent } from './components/transactions-list/transactions-list.component';
import { FilterTransactionTypePipe } from './pipes/filter-transaction-type.pipe';
import { SearchTransactionPipe } from './pipes/search-transaction.pipe';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    TabsComponent,
    TransactionComponent,
    ChartsComponent,
    TransactionsListComponent,
    FilterTransactionTypePipe,
    SearchTransactionPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    NgChartsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
