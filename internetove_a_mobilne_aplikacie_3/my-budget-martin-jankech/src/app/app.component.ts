import { Component } from '@angular/core';
import {AngularFireAuth} from '@angular/fire/auth';
import firebase from 'firebase';




@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent {


  constructor(public afAuth:AngularFireAuth) {
  }

  login() {this.afAuth.signInWithPopup(new firebase.auth.GoogleAuthProvider())

  }

  logout() {
    this.afAuth.signOut();
  }
}


