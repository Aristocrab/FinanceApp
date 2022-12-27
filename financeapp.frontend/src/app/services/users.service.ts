import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { UserDto } from '../models/Users/UserDto';
import { AccountsService } from './accounts.service';
import { ApiService } from './api.service';
import { CategoriesService } from './categories.service';
import { TransactionsService } from './transactions.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService extends ApiService {

  public get isDemoMode(): boolean {
    let userId: string = this.jwtHelper.decodeToken(UsersService.GetJwtToken()).userId;
    return userId.toUpperCase() == "D3AC2B50-6CD3-4D38-8EC8-C8D3827FB3EF";
  }
  @Output() loggedIn = new EventEmitter<boolean>();
  
  constructor(private http: HttpClient, 
    private transactionsService: TransactionsService, 
    private accountsService: AccountsService,
    private categoriesService: CategoriesService,
    private jwtHelper: JwtHelperService) {
    super();
  }
  
  public static IsUserLoggedIn(): boolean {
    return localStorage.getItem('jwtToken') !== null;
  }
  
  public static GetJwtToken(): string {
    return localStorage.getItem('jwtToken') || '';
  }
  
  login(user: UserDto): Observable<string> {
    return this.http.post<string>(`${UsersService.baseUrl}/Users/login`, user, { responseType: 'text' as 'json' })
      .pipe(
        tap(
          (response: string) => {
            localStorage.setItem('jwtToken', response);
            this.loggedIn.emit(true);
          }
        ),
      );
  }
  
  register(user: UserDto): Observable<string> {
    return this.http.post<string>(`${UsersService.baseUrl}/Users/register`, user, { responseType: 'text' as 'json' })
      .pipe(
        tap(
          (response: string) => {
            localStorage.setItem('jwtToken', response);
            this.loggedIn.emit(true);
          }
        ),
      );
  }
  
  logout() {
    localStorage.removeItem('jwtToken');
    this.loggedIn.emit(false);
    this.transactionsService.transactions = [];
    this.accountsService.accounts = [];
    this.categoriesService.categories = [];
  }
}
