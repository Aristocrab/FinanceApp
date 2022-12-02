import { CategoryDto } from "./CategoryDto";

export interface TransactionDto {
    id: string;
    amount: number;
    description: string;
    category: CategoryDto;
    type: number;
    date: Date;        
}
