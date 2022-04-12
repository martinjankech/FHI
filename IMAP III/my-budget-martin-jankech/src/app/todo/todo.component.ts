import { Component, OnInit } from '@angular/core';
import {TodoItem} from './todo-item';
import {TodoService} from './todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styles: [
  ]
})
export class TodoComponent implements OnInit {
  items: TodoItem[];

  constructor(private todoService:TodoService) { }

  ngOnInit(): void {
    this.loadItems();
  }
  loadItems(){this.items=this.todoService.getItems();}

  getRemaining() {
return this.todoService.getRemaining();

  }

  getTotalCount() {
    return this.todoService.getTotalCount();

  }

  removeCompleted() {
    this.todoService.removeCompleted();
    this.loadItems();

  }

  addNewTodoItem(input:HTMLInputElement) {
    if(!input.value){return}
    this.todoService.addNew(new TodoItem(input.value));
    this.loadItems();
    input.value='';



  }

  toggleItem(item: TodoItem) {

      item.completed=item.done ? new Date(): undefined

    this.todoService.toggleItem(item);

  }
}
