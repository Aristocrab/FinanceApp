import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { UserDto } from '../models/Users/UserDto';
import { AccountsService } from './accounts.service';
import { ApiService } from './api.service';
import { CategoriesService } from './categories.service';
import { TransactionsService } from './transactions.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService extends ApiService {

  @Output() loggedIn = new EventEmitter<boolean>();
  
  constructor(private http: HttpClient, 
    private transactionsService: TransactionsService, 
    private accountsService: AccountsService,
    private categoriesService: CategoriesService) {
    super();
  }
  
  public static IsUserLoggedIn(): boolean {
    return localStorage.getItem('jwtToken') !== null;
  }
  
  public static GetJwtToken(): string {
    return localStorage.getItem('jwtToken') || '';
  }
  
  login(user: UserDto) {
    this.http.post<string>(`${UsersService.baseUrl}/Users/login`, user, { responseType: 'text' as 'json' })
      .subscribe(response => {
        localStorage.setItem('jwtToken', response);
        this.loggedIn.emit(true);
      });
  }
  
  register(user: UserDto) {
    this.http.post<string>(`${UsersService.baseUrl}/Users/register`, user, { responseType: 'text' as 'json' })
      .subscribe((response: string) => {
        localStorage.setItem('jwtToken', response);
        this.loggedIn.emit(true);
      });
  }
  
  logout() {
    localStorage.removeItem('jwtToken');
    this.loggedIn.emit(false);
    this.transactionsService.transactions = [];
    this.accountsService.accounts = [];
    this.categoriesService.categories = [];
  }
}
