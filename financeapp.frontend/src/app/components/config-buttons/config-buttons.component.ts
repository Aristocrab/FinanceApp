import { Component, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-buttons',
  templateUrl: './config-buttons.component.html',
  styleUrls: ['./config-buttons.component.css']
})
export class ConfigButtonsComponent {

  constructor(private modalService: NgbModal, private usersService: UsersService) {
  }
  
  openModal(modal: any) {
    this.modalService.open(modal, {
      centered: true,
    });
  }
  
  closeModal(modal: any) {
    this.modalService.dismissAll(modal);
  }
  
  logout() {
    this.usersService.logout();
  }
}
