import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-new-transaction',
  templateUrl: './new-transaction.component.html',
  styleUrls: ['./new-transaction.component.css']
})
export class NewTransactionComponent {
  
  public get isDemoMode(): boolean {
    return this.usersService.isDemoMode;
  }

  constructor(private modalService: NgbModal, private usersService: UsersService) {
  }

  open(content: any) {
		this.modalService.open(content, { 
      ariaLabelledBy: 'modal-basic-title',
      centered: true 
    });
	}
}
