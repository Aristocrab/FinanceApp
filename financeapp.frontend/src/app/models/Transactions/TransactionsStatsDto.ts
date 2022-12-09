import { TransactionType } from "./TransactionType";

export interface TransactionsStatsDto {
    timePeriod: string;
    expensesSum: number;
    incomeSum: number;
}
