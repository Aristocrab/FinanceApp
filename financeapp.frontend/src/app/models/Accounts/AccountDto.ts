import { Guid } from "guid-typescript";

export interface AccountDto {
    id: string;
    name: string;
    balance: number;
    currency: string;
    transactionsCount: number;
    icon: number;
}
