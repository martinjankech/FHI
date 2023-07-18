import { Component, OnInit } from '@angular/core';
import {BudgetItem} from '../budget-item';
import {BudgetItemsService} from '../budget-items.service';
import {Router} from '@angular/router';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-new-item',
  templateUrl: './new-item.component.html',
  styles: [
  ]
})
export class NewItemComponent implements OnInit {
  newItem = {} as BudgetItem;
  constructor(
    private budgetItemsService: BudgetItemsService,
    public activeModal: NgbActiveModal
  )
  {

  }

  submit($event):void {

    this.budgetItemsService.add($event).then(value =>
    {this.activeModal.close($event)})}






  ngOnInit(): void {
  }

  close() {
    this.activeModal.close("Closed");

  }
}
