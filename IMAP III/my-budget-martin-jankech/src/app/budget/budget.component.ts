import {Component, OnInit} from '@angular/core';
import {BudgetItem} from './budget-item';
import {BudgetItemsService} from './budget-items.service';
import {Observable} from 'rxjs';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {NewItemComponent} from './new-item/new-item.component';

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
})
export class BudgetComponent implements OnInit {

  items$: Observable<BudgetItem[]>;



  constructor(private budgetItemsService: BudgetItemsService,
  private modalService: NgbModal) {
    this.items$ = budgetItemsService.getItems()
  }

  ngOnInit(): void {
  }



  deleteItem(item: BudgetItem) {
    this.budgetItemsService.delete(item).then(value => {}).catch(reason => {alert(reason.message)})
  }

  newBudget() {
    const modalRef = this.modalService.open(NewItemComponent);
    modalRef.result.then(value => {console.log(value)}).catch(reason => {console.log(reason)})


  }
}
