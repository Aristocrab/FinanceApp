import { Component, OnInit } from '@angular/core';
import { AccountsService } from './services/accounts.service';
import { CategoriesService } from './services/categories.service';
import { TransactionsService } from './services/transactions.service';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  constructor(private accountsService: AccountsService, 
    private transactionsService: TransactionsService,
    private categoriesService: CategoriesService,
    private userService: UsersService
    ) { }

  ngOnInit(): void {    
    this.accountsService.selectedAccountUpdated.subscribe(() => {
      this.accountsService.fetchAccounts();
      this.transactionsService.fetchTransactions();
    });
    
    this.transactionsService.incomeTransactionsUpdated.subscribe(() => {
      this.accountsService.fetchAccounts();
    });
    
    this.transactionsService.expensesTransactionsUpdated.subscribe(() => {
      this.accountsService.fetchAccounts();
    });
    
    this.categoriesService.categoriesUpdated.subscribe(() => {
      this.transactionsService.fetchTransactions();
      this.categoriesService.fetchCategories();
    });
  }

  isUserLoggedIn(): boolean {
    return UsersService.IsUserLoggedIn();
  }
}
