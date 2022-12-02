import { Component, Input, OnInit } from '@angular/core';
import { TransactionDto } from 'src/app/models/TransactionDto';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit {
  
  @Input() transactions: TransactionDto[] = [];
  
  constructor() { 
  }
  
  ngOnInit(): void {
  }
}
