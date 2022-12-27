import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { IconName } from 'ngx-bootstrap-icons';
import { AccountDto } from '../models/Accounts/AccountDto';
import { CreateAccountDto } from '../models/Accounts/CreateAccountDto';
import { DeleteAccountDto } from '../models/Accounts/DeleteAccountDto';
import { UpdateAccountDto } from '../models/Accounts/UpdateAccountDto';
import { UserAccountsDto } from '../models/Accounts/UserAccountsDto';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AccountsService extends ApiService {
    
  public selectedAccount: AccountDto | undefined;
  public accounts: AccountDto[] | undefined;
  public accountsBalanceSum: number | undefined;
  
  public icons: IconName[] = [
    "cash", 
    "credit-card",
    "currency-dollar",
    "currency-euro",
    "piggy-bank",
  ];
  
  selectedAccountUpdated = new EventEmitter();
  
  constructor(private http: HttpClient) {
    super();
  }
  
  fetchAccounts() {
    this.getAccounts().subscribe(accounts => {
      this.accounts = accounts.accounts;
      this.accountsBalanceSum = accounts.accountsBalanceSum;
    });
  }
  
  getAccounts() {
    return this.http.get<UserAccountsDto>(`${ApiService.baseUrl}/Accounts`);
  }
  
  createAccount(account: CreateAccountDto) {
    return this.http.post(`${ApiService.baseUrl}/Accounts/new`, account);
  }
  
  updateAccount(account: UpdateAccountDto) {
    return this.http.put(`${ApiService.baseUrl}/Accounts/update`, account);
  }
  
  deleteAccount(account: DeleteAccountDto) {
    return this.http.delete(`${ApiService.baseUrl}/Accounts/delete`, {
      body: account
    });
  }
  
}
