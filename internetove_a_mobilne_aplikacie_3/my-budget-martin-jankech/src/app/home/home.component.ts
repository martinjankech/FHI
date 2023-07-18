import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent implements OnInit {

  name = 'Martin';
  counterValue = 0;


  constructor() {
  }

  ngOnInit(): void {
  }

  increment() {
    this.counterValue++;

  }
}
