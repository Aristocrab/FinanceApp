import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
  selectedAccountId: string | undefined;
  
  constructor(public transactionsService: TransactionsService, 
    public accountsService: AccountsService,
    private modalService: NgbModal) { }
  
  ngOnInit(): void {    
    this.transactionsService.incomeTransactionsUpdated.subscribe(() => {
      this.onAccountChange();
    });
    
    this.transactionsService.expensesTransactionsUpdated.subscribe(() => {
      this.onAccountChange();
    });
  }
  
  onAccountChange() {
    if(this.selectedAccountId) {
      this.accountsService.selectedAccount = this.accountsService.accounts?.find(a => a.id === this.selectedAccountId);
    } else {
      this.accountsService.selectedAccount = undefined;
    }
    if(!this.accountsService.selectedAccount) {
      this.selectedAccountId = undefined;
    }
    this.accountsService.selectedAccountUpdated.emit();
  }
  
  open(content: any) {
		this.modalService.open(content, { 
      ariaLabelledBy: 'modal-basic-title',
      centered: true 
    });
	}

}
