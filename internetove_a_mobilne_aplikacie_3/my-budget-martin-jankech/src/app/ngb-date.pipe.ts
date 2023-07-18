import { Pipe, PipeTransform } from '@angular/core';
import {NgbDateParserFormatter, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Pipe({
  name: 'ngbDate'
})
export class NgbDatePipe implements PipeTransform {
  constructor(private ngbFormatter:NgbDateParserFormatter) {
  }

  transform(value: NgbDateStruct, ...args: unknown[]): unknown {
    console.log(value);
    //return this.ngbFormatter.format(value);
    if(!value)
    {return }
    return value.day+"."+value.month+"."+value.year;
  }

}
