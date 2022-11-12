import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { map, Observable, of } from 'rxjs';
import IUser, { Position } from '../../models/IUser';
import { SystemAdministratorService } from '../../services/SystemAdministratorService';

@Injectable({
  providedIn: 'root'
})
export class AvailableProfessorsResolver implements Resolve<IUser[]> {
  constructor(private service: SystemAdministratorService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUser[]> {
    return this.service.getAllUsers().pipe(map((res: IUser[]) => {
      return res.filter(u => u.position === Position.Assistant || u.position === Position.Professor);
    }));
  }
}
