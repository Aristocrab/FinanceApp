import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-accounts-modal',
  templateUrl: './accounts-modal.component.html',
  styleUrls: ['./accounts-modal.component.css']
})
export class AccountsModalComponent {
  
  @Output() close = new EventEmitter();
  
  accountId: string | undefined;
  name: string | undefined;
  balance: number | undefined = 0;
  currency: number = 0;
  
  modalState: 'Create' | 'Update' = 'Create';
  
  constructor(public accountsService: AccountsService, private transactionsService: TransactionsService) 
  { }
  
  buttonClicked(form: NgForm, event: any) {
    if(!form.form.valid) {
      form.form.markAllAsTouched();
      event.preventDefault();
    }
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
      this.currency = 0;
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
  
  submitForm(form: NgForm) {
    if (this.modalState === 'Create') {
      this.createAccount();
    } else {
      this.updateAccount();
    }
    form.form.markAsUntouched();
    form.form.markAsPristine();
  }
  
  createAccount() {
    this.accountsService.createAccount({
      name: this.name!,
      balance: this.balance!,
      currency: this.currency!
    }).subscribe(() => {
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
      this.accountsService.selectedAccountUpdated.emit();
      
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
      if(this.accountsService.selectedAccount?.id === accountId) {
        this.accountsService.selectedAccount = undefined;
      }
      this.accountsService.selectedAccountUpdated.emit();
    });
  }
}
