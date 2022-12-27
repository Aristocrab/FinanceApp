import { AccountDto } from "./AccountDto";

export interface UserAccountsDto {
    accountsBalanceSum: number;
    accounts: AccountDto[];
}
