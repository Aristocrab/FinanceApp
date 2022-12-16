import { Pipe, PipeTransform } from '@angular/core';
import { AccountDto } from '../models/Accounts/AccountDto';

@Pipe({
  name: 'sumAccountsBalances'
})
export class SumAccountsBalancesPipe implements PipeTransform {
  
  transform(value: AccountDto[]): number {
    if (!value) {
      return 0;
    }
  
    return value
    .map(a => {
      if(a.currency == 'UAH') return a.balance;
      if(a.currency == 'USD') return a.balance * 36;
      if(a.currency == 'EUR') return a.balance * 38;
      return 0;
    })
    .reduce((a, b) => a + b, 0);
  }

}
