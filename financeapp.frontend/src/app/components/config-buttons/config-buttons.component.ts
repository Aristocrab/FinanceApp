import { Component, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-buttons',
  templateUrl: './config-buttons.component.html',
  styleUrls: ['./config-buttons.component.css']
})
export class ConfigButtonsComponent {

  constructor(private modalService: NgbModal) {
  }
  
  openModal(modal: any) {
    this.modalService.open(modal, {
      centered: true,
    });
  }
  
  closeModal(modal: any) {
    this.modalService.dismissAll(modal);
  }
}
