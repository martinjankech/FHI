import { Injectable } from '@angular/core';
import {TodoItem} from './todo-item';
import {LocalStorageService} from 'angular-2-local-storage';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private items:TodoItem[];
private  key='todo-items';
  constructor(private  localStorageService:LocalStorageService)
  {  this.items=localStorageService.get(this.key) || []}

  getItems():TodoItem[]{
    return this.items;
  }
  getRemaining() {
    let count =0;
    this.items.forEach(value=>{count+=value.done ? 0:1
    });
    return count;
  }
  getTotalCount() {
    return this.items.length;

  }
  removeCompleted() {
    const newItem:TodoItem[]=[];
    this.items.forEach(value => {if(!value.done)
      newItem.push(value)
    })
    this.items=newItem;
    this.localStorageService.set(this.key,newItem);

  }
  addNew(item:TodoItem){

    this.items.push(item);
    this.localStorageService.set(this.key,this.items);
  }

  toggleItem(Item: TodoItem) {
    this.localStorageService.set(this.key,this.items);

  }
}
