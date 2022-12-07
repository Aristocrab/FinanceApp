import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AccountDto } from '../models/Accounts/AccountDto';
import { CreateAccountDto } from '../models/Accounts/CreateAccountDto';
import { DeleteAccountDto } from '../models/Accounts/DeleteAccountDto';
import { UpdateAccountDto } from '../models/Accounts/UpdateAccountDto';
import { AlertsService } from './alerts.service';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AccountsService extends ApiService {
    
  public selectedAccount: AccountDto | undefined;
  
  selectedAccountUpdated = new EventEmitter();
  
  constructor(private http: HttpClient,
    private alertsService: AlertsService
    ) {
    super();
  }
  
  getAccounts() {
    return this.http.get<AccountDto[]>(`${ApiService.baseUrl}/Accounts`);
  }
  
  createAccount(account: CreateAccountDto) {
    return this.http.post(`${ApiService.baseUrl}/Accounts/new`, account)
    .pipe(
      catchError(err => {
        this.alertsService.addAlert('warning', err.error);
        return throwError(() => new Error('Something bad happened; please try again later.'));
      })
    );
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
