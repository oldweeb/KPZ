import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DayOfWeek, EventType } from 'src/app/models/IEvent';
import IGroup from 'src/app/models/IGroup';
import IUser from 'src/app/models/IUser';
import { IEventDto, SystemAdministratorService } from 'src/app/services/SystemAdministratorService';
import { AvailableProfessorsResolver } from '../resolvers/available-professors.resolver';

@Component({
  selector: 'app-new-event',
  templateUrl: './new-event.component.html',
  styleUrls: ['./new-event.component.scss'],
  providers: [AvailableProfessorsResolver]
})
export class NewEventComponent implements OnInit {
  availableGroups: IGroup[] = [];
  availableProfessors: IUser[] = [];
  event: Partial<IEventDto> = {};

  eventTypes = EventType;
  typeKeys: string[] = [];

  days = DayOfWeek;
  dayKeys: string[] = [];

  constructor(private service: SystemAdministratorService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.typeKeys = Object.keys(this.eventTypes).filter(k => !Number.isNaN(+k));
    this.dayKeys = Object.keys(this.days).filter(k => !Number.isNaN(+k) && +k !== 0);
  }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ availableGroups, availableProfessors }) => {
      this.availableGroups = availableGroups;
      this.availableProfessors = availableProfessors;
    });
  }

  create(): void {
    this.service.createEvent(this.event as IEventDto).subscribe(
      () => {
        alert('Event successfully created.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`Event creation failed. Description: ${error}`);
      }
    );
  }

  cancel(): void {
    this.router.navigateByUrl('admin');
  }
}
