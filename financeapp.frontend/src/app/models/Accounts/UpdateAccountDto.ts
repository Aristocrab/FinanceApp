import { Guid } from "guid-typescript";

export interface UpdateAccountDto {
    accountId: string;
    name: string;
    balance: number;
    currency: number;
}
