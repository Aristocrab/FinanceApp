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
import { FilterTransactionsByTypePipe } from './pipes/filter-transactions-by-type.pipe';
import { SearchTransactionPipe } from './pipes/search-transaction.pipe';
import { FormsModule } from '@angular/forms';
import { NgxBootstrapIconsModule, allIcons } from 'ngx-bootstrap-icons';
import { NewTransactionComponent } from './components/new-transaction/new-transaction.component';
import { SumAccountsBalancesPipe } from './pipes/sum-accounts-balances.pipe';
import { FilterTransactionsByAccountPipe } from './pipes/filter-transactions-by-account.pipe';
import { ConfigButtonsComponent } from './components/config-buttons/config-buttons.component';
import { AccountsModalComponent } from './components/modals/accounts-modal/accounts-modal.component';
import { CategoriesModalComponent } from './components/modals/categories-modal/categories-modal.component';
import { TransferModalComponent } from './components/modals/transfer-modal/transfer-modal.component';
import { CreateTransactionModalComponent } from './components/modals/create-transaction-modal/create-transaction-modal.component';
import { UpdateTransactionModalComponent } from './components/modals/update-transaction-modal/update-transaction-modal.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthModalComponent } from './components/modals/auth-modal/auth-modal.component';
import { MainComponent } from './components/main/main.component';
import { ApiService } from './services/api.service';

@NgModule({
  declarations: [
    AppComponent,
    TabsComponent,
    TransactionComponent,
    ChartsComponent,
    TransactionsListComponent,
    FilterTransactionsByTypePipe,
    SearchTransactionPipe,
    NewTransactionComponent,
    SumAccountsBalancesPipe,
    FilterTransactionsByAccountPipe,
    ConfigButtonsComponent,
    AccountsModalComponent,
    CategoriesModalComponent,
    TransferModalComponent,
    CreateTransactionModalComponent,
    UpdateTransactionModalComponent,
    AuthModalComponent,
    MainComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    
    NgbModule,
    NgChartsModule,
    NgxBootstrapIconsModule.pick(allIcons),
    
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem("jwtToken"),
        allowedDomains: [ApiService.BackendUrl],
      },
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
