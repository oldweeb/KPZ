import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import IEvent, { DayOfWeek, EventType } from 'src/app/models/IEvent';
import IGroup from 'src/app/models/IGroup';
import IUser from 'src/app/models/IUser';
import { IEventDto, SystemAdministratorService } from 'src/app/services/SystemAdministratorService';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.scss']
})
export class EditEventComponent implements OnInit {
  events: IEvent[] = [];
  event: Partial<IEventDto> = {};
  selectedId: string | null = null;

  availableGroups: IGroup[] = [];
  availableProfessors: IUser[] = [];

  eventTypes = EventType;
  typeKeys: string[] = [];

  days = DayOfWeek;
  dayKeys: string[] = [];

  constructor(private service: SystemAdministratorService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.typeKeys = Object.keys(this.eventTypes).filter(k => !Number.isNaN(+k));
    this.dayKeys = Object.keys(this.days).filter(k => !Number.isNaN(+k) && +k !== 0);
  }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ events, availableGroups, availableProfessors }) => {
      this.events = events;
      this.availableGroups = availableGroups;
      this.availableProfessors = availableProfessors;
    });
  }

  update(): void {
    debugger;
    this.service.updateEvent(this.event as IEventDto).subscribe(
      () => {
        alert('Event has been successfully updated.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`Event updating failed. Description: ${error}`);
      }
    );
  }

  onChangeEvent(ev: any): void {
    this.selectedId = ev.target.selectedOptions[0].value;
    const event = this.events.filter(e => e.id === this.selectedId)[0];
    this.event = {
      id: event.id,
      order: event.order,
      professorId: event.professor.id,
      groupId: event.group.id,
      type: event.type,
      name: event.name,
      dayOfWeek: event.dayOfWeek
    };
  }

  cancel(): void {
    this.router.navigateByUrl('admin');
  }

  delete(): void {
    this.service.deleteEvent(this.event.id!).subscribe(
      () => {
        alert('Event has been successfully deleted.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`Event deletion failed. Description: ${error}`);
      }
    )
  }
}
