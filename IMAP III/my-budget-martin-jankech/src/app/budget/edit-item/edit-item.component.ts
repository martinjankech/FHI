import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {BudgetItemsService} from '../budget-items.service';
import {BudgetItem} from '../budget-item';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styles: [
  ]
})
export class EditItemComponent implements OnInit {
  private itemId: string;
   item: BudgetItem;


  constructor(  private route: ActivatedRoute,
                private budgetItemService: BudgetItemsService,
                private router:Router) {
    this.itemId = this.route.snapshot.paramMap.get("id");
budgetItemService.getItem(this.itemId).subscribe(value =>
{
  this.item=value;
})
  }

  ngOnInit(): void {
  }

  submit($event:BudgetItem) {
    this.budgetItemService.edit(this.itemId, $event).then(value => {

      this.router.navigate(['/budget',this.itemId]);

    })


  }
}
