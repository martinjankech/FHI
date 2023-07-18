import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {TodoComponent} from './todo/todo.component';
import {Error404Component} from './error404/error404.component';
import {CalcComponent} from './calc/calc.component';
import {BudgetComponent} from './budget/budget.component';
import {NewItemComponent} from './budget/new-item/new-item.component';
import {ItemDetailComponent} from './budget/item-detail/item-detail.component';
import {EditItemComponent} from './budget/edit-item/edit-item.component';
import {AngularFireAuthGuard, redirectUnauthorizedTo} from '@angular/fire/auth-guard';
import {AuthorizedGuard} from './authorized-guard';
import {NavBarComponent} from './nav-bar/nav-bar.component';
import {MapLocationComponent} from './map-location/map-location.component';




const redirectUnauthorizedToHome = () => redirectUnauthorizedTo(['home']);

const routes: Routes = [
  {path:'home', component:HomeComponent},
  {path: 'todo', component:TodoComponent},
  {path: 'calc', component: CalcComponent},
  {path: 'navbar', component: NavBarComponent},
  {
    path: 'budget', component: BudgetComponent,
    canActivate:[AuthorizedGuard]},
  {path: 'budget/:id', component: ItemDetailComponent,canActivate:[AuthorizedGuard]},
  {path: 'budget/:id/edit', component: EditItemComponent},
  {path: 'map', component: MapLocationComponent},
  {path:'', redirectTo: '/home', pathMatch:'full'},
  {path:'**', component: Error404Component},

];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
