import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {BudgetItem} from '../budget-item';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-budget-item-form',
  templateUrl: './budget-item-form.component.html',
  styles: [
  ]
})
export class BudgetItemFormComponent implements OnInit{
 @Input() item: BudgetItem;
 @Output() changed=new EventEmitter<BudgetItem>();
 buttonVisible: boolean;

ngOnInit() {this.buttonVisible=this.changed.observers.length > 0;
}

  submit(formElement:NgForm) {
    if(formElement.invalid)
    {return}
    this.changed.emit(this.item);

  }
}
