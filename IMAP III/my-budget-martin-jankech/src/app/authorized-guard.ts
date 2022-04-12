import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {AngularFireAuth} from '@angular/fire/auth';
import {Injectable} from '@angular/core';
@Injectable()
export class AuthorizedGuard implements CanActivate
{
  constructor(private router:Router,private auth:AngularFireAuth) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    return new Observable<boolean| UrlTree>(subscriber => {this.auth.user.subscribe(user => {
      if(user){subscriber.next(true)

      }else
      {
      subscriber.next(this.router.parseUrl('/home'))}

      subscriber.complete()
    })})
  }
}
