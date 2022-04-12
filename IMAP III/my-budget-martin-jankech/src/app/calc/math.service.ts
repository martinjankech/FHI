import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MathService {

  constructor() { }
  public add(a: number, b:number) : number
{
  return a + b ;
}
  public sub(a: number, b:number) : number
  {
    return a - b ;
  }
  public div(a: number, b:number) : number
  {
    return a / b ;
  }
  public percent(a: number, b:number) : number
  {
    return (((a / 100)* b))
  }
}
