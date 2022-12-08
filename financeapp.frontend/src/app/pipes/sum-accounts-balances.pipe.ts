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
    .map(a => a.balance)
    .reduce((a, b) => a + b, 0);
  }

}
