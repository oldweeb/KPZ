import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import IGroup from '../../models/IGroup';
import { SystemAdministratorService } from '../../services/SystemAdministratorService';

@Injectable({
  providedIn: 'root'
})
export class AvailableGroupsResolver implements Resolve<IGroup[]> {
  constructor(private service: SystemAdministratorService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IGroup[]> {
    return this.service.getAllGroups();
  }
}
