import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { CategoriesService } from 'src/app/services/categories.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-accounts-button',
  templateUrl: './accounts-button.component.html',
  styleUrls: ['./accounts-button.component.css']
})
export class AccountsButtonComponent implements OnInit {
  accounts: AccountDto[] | undefined;
  
  accountId: string | undefined;
  name: string | undefined;
  balance: number | undefined = 0;
  currency: number | undefined = 1;
  
  modalState: 'Create' | 'Update' = 'Create';
  
  constructor(private modalService: NgbModal,
    private accountsService: AccountsService,
    private transactionsService: TransactionsService
  ) { }
  
  ngOnInit(): void {
    this.accountsService.getAccounts().subscribe(result => {
      this.accounts = result;
    });
  }
  
  open(content: any) {
		this.modalService.open(content, { 
      ariaLabelledBy: 'modal-basic-title',
      centered: true 
    });
	}
  
  changeModalState(state: 'Create' | 'Update', account: AccountDto | undefined = undefined) {
    this.modalState = state;
    
    if (state === 'Update') {
      this.accountId = account!.id;
      this.name = account!.name;
      this.balance = account!.balance;
      this.currency = this.convertCurrencyToNumber(account!.currency);
    } else {
      this.accountId = undefined;
      this.name = undefined;
      this.balance = 0;
      this.currency = 1;
    }
  }
  
  convertCurrencyToNumber(currency: string) {
    switch (currency) {
      case 'UAH':
        return 0;
      case 'USD':
        return 1;
      case 'EUR':
      default:
        return 2;
    }
  }
  
  createAccount() {
    this.accountsService.createAccount({
      name: this.name!,
      balance: this.balance!,
      currency: this.currency!
    }).subscribe(() => {
      this.accountsService.getAccounts().subscribe(result => {
        this.accounts = result;
      });
      
      this.accountsService.selectedAccountUpdated.emit();
      
      this.accountId = undefined;
      this.name = undefined;
      this.balance = 0;
      this.currency = 1;
    });
  }
  
  updateAccount() {
    this.accountsService.updateAccount({
      accountId: this.accountId!,
      name: this.name!,
      balance: this.balance!,
      currency: this.currency!
    }).subscribe(() => {
      this.accountsService.getAccounts().subscribe(result => {
        this.accounts = result;
      });
      
      this.accountsService.selectedAccountUpdated.emit();
      this.transactionsService.fetchTransactions();
      
      this.accountId = undefined;
      this.name = undefined;
      this.balance = 0;
      this.currency = 1;
      this.changeModalState('Create');
    });
  }
  
  deleteAccount(accountId: string) {
    this.accountsService.deleteAccount({
      accountId: accountId
    }).subscribe(() => {
      this.accountsService.getAccounts().subscribe(result => {
        this.accounts = result;
      });
      
      this.accountsService.selectedAccountUpdated.emit();
      this.transactionsService.fetchTransactions();
    });
  }
}
