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
import { NewTransactionComponent } from './components/new-transaction/new-transaction.component';
import { SumAccountsBalancesPipe } from './pipes/sum-accounts-balances.pipe';
import { FilterTransactionsByAccountPipe } from './pipes/filter-transaction-by-account.pipe';
import { ButtonsComponent } from './components/buttons/buttons.component';
import { CategoriesButtonComponent } from './components/buttons/categories-button/categories-button.component';
import { AccountsButtonComponent } from './components/buttons/accounts-button/accounts-button.component';
import { AlertsComponent } from './components/alerts/alerts.component';
@NgModule({
  declarations: [
    AppComponent,
    TabsComponent,
    TransactionComponent,
    ChartsComponent,
    TransactionsListComponent,
    FilterTransactionTypePipe,
    SearchTransactionPipe,
    NewTransactionComponent,
    SumAccountsBalancesPipe,
    FilterTransactionsByAccountPipe,
    ButtonsComponent,
    CategoriesButtonComponent,
    AccountsButtonComponent,
    AlertsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    
    NgbModule,
    NgChartsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
