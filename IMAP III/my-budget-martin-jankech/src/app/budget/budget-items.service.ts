import {Injectable} from '@angular/core';
import {BudgetItem} from './budget-item';
import {AngularFirestore} from '@angular/fire/firestore';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BudgetItemsService {



  constructor(private firestore:AngularFirestore) {


  }
  private readonly path="items"

  getItems() {
    return this.firestore.collection<BudgetItem>(this.path).valueChanges({idField:'id'});
  }

  add(newItem: BudgetItem) {
    return this.firestore.collection<BudgetItem>(this.path).add(newItem);
  }

  delete(item: BudgetItem) {
    return this.firestore.collection<BudgetItem>(this.path).doc(item.id).delete();
    }


  getItem(itemId: string): Observable <BudgetItem> {
  return this.firestore.collection<BudgetItem>(this.path).doc(itemId).valueChanges({idField:'id'});
  }

  edit(itemId: string, item: BudgetItem) :Promise<void> {
    return this.firestore.collection<BudgetItem>(this.path).doc(itemId).set(item);



  }
}
