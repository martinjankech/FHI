import {DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {FormsModule} from '@angular/forms';
import { CalcComponent } from './calc/calc.component';
import { HomeComponent } from './home/home.component';
import { TodoComponent } from './todo/todo.component';
import { Error404Component } from './error404/error404.component';
import {LocalStorageModule} from 'angular-2-local-storage';
import {registerLocaleData} from '@angular/common';
import localeSk from '@angular/common/locales/sk';
import { BudgetComponent } from './budget/budget.component';
import { NewItemComponent } from './budget/new-item/new-item.component';
import { ItemDetailComponent } from './budget/item-detail/item-detail.component';
import { EditItemComponent } from './budget/edit-item/edit-item.component';
import {AngularFireModule} from '@angular/fire';
import {environment} from '../environments/environment';
import {AngularFirestoreModule} from '@angular/fire/firestore';
import { BudgetItemFormComponent } from './budget/budget-item-form/budget-item-form.component';
import {NgbDatepickerModule, NgbModalModule,} from '@ng-bootstrap/ng-bootstrap';
import {AngularFireAuthModule} from '@angular/fire/auth';
import { NgbDatePipe } from './ngb-date.pipe';
import {AngularFireAuthGuard, AngularFireAuthGuardModule} from '@angular/fire/auth-guard';
import {AuthorizedGuard} from './authorized-guard';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { MapLocationComponent } from './map-location/map-location.component';
import {AgmCoreModule} from '@agm/core';



registerLocaleData(localeSk)
@NgModule({
  declarations: [
    AppComponent,
    CalcComponent,
    HomeComponent,
    TodoComponent,
    Error404Component,
    BudgetComponent,
    NewItemComponent,
    ItemDetailComponent,
    EditItemComponent,
    BudgetItemFormComponent,

    NgbDatePipe,
    NavBarComponent,
    MapLocationComponent,



  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    LocalStorageModule.forRoot({
      prefix: 'my-app',
      storageType: 'localStorage'
    }),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyB9Tb6xIQifH7sByZCfmE8KWManTcO9DgA'
    }),

    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule,
    AngularFireAuthModule,
    NgbModalModule,
    AngularFireAuthGuardModule,
    NgbDatepickerModule,





  ],
  providers: [
    {provide:LOCALE_ID, useValue:'sk'},
    {provide:DEFAULT_CURRENCY_CODE,useValue:'EUR'},
AuthorizedGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
