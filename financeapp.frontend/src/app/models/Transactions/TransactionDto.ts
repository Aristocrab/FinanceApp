import { Guid } from "guid-typescript";
import { AccountDto } from "../Accounts/AccountDto";
import { CategoryDto } from "../Categories/CategoryDto";
import { TransactionType } from "./TransactionType";

export interface TransactionDto {
    id: string;
    amount: number;
    description: string;
    category: CategoryDto | null;
    account: AccountDto | null;
    type: TransactionType;
    date: Date;        
}
