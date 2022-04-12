import {Component, Input, OnInit} from '@angular/core';
import {AngularFireAuth} from '@angular/fire/auth';
import firebase from 'firebase';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styles: [
  ]
})
export class NavBarComponent implements OnInit {
    @Input() menuitems =
     [{label:'Home',link:'/home'},
    {label:'Calc',link:'/calc'},
    {label:'Todo',link:'/todo'},
    {label:'Budget',link:'/budget'},
       {label:'Map',link:'/map'}
    ]


  constructor(public afAuth:AngularFireAuth) { }

  ngOnInit(): void {
  }

  logout() {
    this.afAuth.signOut();

  }

  login() {
    this.afAuth.signInWithPopup(new firebase.auth.GoogleAuthProvider())

  }
}
