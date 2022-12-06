import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountDto } from 'src/app/models/Accounts/AccountDto';
import { AccountsService } from 'src/app/services/accounts.service';
import { TransactionsService } from 'src/app/services/transactions.service';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.css']
})
export class TabsComponent implements OnInit {

  active = -1;
  searchText: string | undefined;
  accounts: AccountDto[] | undefined;
  selectedAccountId: string | undefined;
  
  amount: number | undefined;
  accountFromId: string | undefined;
  accountToId: string | undefined;
  date: Date | undefined;
  description: string | undefined;
  
  constructor(public transactionsService: TransactionsService, 
    public accountsService: AccountsService,
    private modalService: NgbModal) { }
  
  ngOnInit(): void {
    this.transactionsService.fetchTransactions();
    this.accountsService.getAccounts().subscribe(accounts => this.accounts = accounts);
    
    this.transactionsService.incomeTransactionsUpdated.subscribe(() => {
      this.onAccountChange();
    });
    
    this.transactionsService.expensesTransactionsUpdated.subscribe(() => {
      this.onAccountChange();
    });
    
    this.accountsService.selectedAccountUpdated.subscribe(() => {
      this.accountsService.getAccounts().subscribe(accounts => this.accounts = accounts);
    });
  }
  
  onAccountChange() {
    this.accountsService.getAccounts().subscribe(accounts => this.accounts = accounts);
    if(this.selectedAccountId) {
      this.accountsService.selectedAccount = this.accounts?.find(a => a.id === this.selectedAccountId);
    }
    this.accountsService.selectedAccountUpdated.emit();
  }
  
  open(content: any) {
		this.modalService.open(content, { 
      ariaLabelledBy: 'modal-basic-title',
      centered: true 
    });
	}
  
  transferTransaction() {
    this.transactionsService.transferTransaction({
      amount: this.amount!,
      accountFromId: this.accountFromId!,
      accountToId: this.accountToId!,
      date: this.date!,
      description: this.description!
    }).subscribe(() => {
      this.modalService.dismissAll();
      this.transactionsService.fetchTransactions();
      
      this.transactionsService.incomeTransactionsUpdated.emit();
      this.transactionsService.expensesTransactionsUpdated.emit();
    });
  }

}
