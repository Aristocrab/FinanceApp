import { Component, OnInit } from '@angular/core';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  
  constructor(private accountsService: AccountsService, 
    private transactionsService: TransactionsService,
    private categoriesService: CategoriesService,
  ) {
    this.accountsService.fetchAccounts();
    this.categoriesService.fetchCategories();
    this.transactionsService.fetchTransactions();
   }
  
  ngOnInit(): void {
  }
  
}
