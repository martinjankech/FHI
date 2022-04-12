import { Component, OnInit } from '@angular/core';
import {MathService} from './math.service';

@Component({
  selector: 'app-calc',
  templateUrl: './calc.component.html',
  styles: [
  ]
})
export class CalcComponent implements OnInit {
valueA: number =0;
valueB: number =0;
result: number =0;
  constructor(private mathService:MathService) { }

  ngOnInit(): void {
  }
  add(){
    this.result=this.mathService.add(this.valueA,this.valueB);
  }
  sub(){
    this.result=this.mathService.sub(this.valueA,this.valueB)
  }

  div() {this.result=this.mathService.div(this.valueA,this.valueB)

  }

  percent() {this.result=this.mathService.percent(this.valueA,this.valueB)

  }
}
