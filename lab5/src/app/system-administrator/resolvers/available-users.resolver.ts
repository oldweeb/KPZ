import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { map, Observable, of } from 'rxjs';
import IUser from 'src/app/models/IUser';
import { SystemAdministratorService } from 'src/app/services/SystemAdministratorService';

@Injectable({
  providedIn: 'root'
})
export class AvailableUsersResolver implements Resolve<IUser[]> {
  constructor(private service: SystemAdministratorService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUser[]> {
    return this.service.getAllUsers();
  }
}
