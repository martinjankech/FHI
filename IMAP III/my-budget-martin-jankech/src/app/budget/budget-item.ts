import {NgbDatePipe} from '../ngb-date.pipe';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

export interface BudgetItem {
  id?:string,
  name:string,
  value:number,
  date:NgbDateStruct;

}
