import { Component, Input, OnInit } from '@angular/core';
import { TransactionDto } from 'src/app/models/TransactionDto';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  @Input() transaction!: TransactionDto;
  
  constructor() { }

  ngOnInit(): void {
    
  }

}
