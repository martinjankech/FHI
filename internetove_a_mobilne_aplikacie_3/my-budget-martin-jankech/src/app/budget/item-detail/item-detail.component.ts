import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {BudgetItemsService} from '../budget-items.service';
import {BudgetItem} from '../budget-item';
import {NgbDatePipe} from '../../ngb-date.pipe';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  providers:[NgbDatePipe]
})
export class ItemDetailComponent implements OnInit {
   item: BudgetItem;
  private itemId:string;
  itemDate: any;



  constructor(private route:ActivatedRoute,private budgetItemService: BudgetItemsService,
              private ngbDatePipe: NgbDatePipe) {
    this.itemId=this.route.snapshot.paramMap.get("id");
budgetItemService.getItem(this.itemId).subscribe(value =>
{this.item=value;
this.itemDate=ngbDatePipe.transform(value.date)}
);

  }

  ngOnInit(): void {
  }

}

