import { Guid } from "guid-typescript";

export interface TransferTransactionDto {
    accountFromId: string;
    accountToId: string;
    
    amount: number;
    description: string;
    date: string;        
}
