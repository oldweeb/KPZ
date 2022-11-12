import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import IEvent from 'src/app/models/IEvent';
import { SystemAdministratorService } from 'src/app/services/SystemAdministratorService';

@Injectable({
  providedIn: 'root'
})
export class AvailableEventsResolver implements Resolve<IEvent[]> {
  constructor(private service: SystemAdministratorService) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IEvent[]> {
    return this.service.getAllEvents();
  }
}
