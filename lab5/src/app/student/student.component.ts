import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import IEvent, { DayOfWeek } from '../models/IEvent';
import { TokenStorageService } from '../services/TokenStorageService';
import { UserService } from '../services/UserService';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss'],
  providers: [UserService]
})
export class StudentComponent implements OnInit {
  events?: IEvent[];
  error?: string;
  day: DayOfWeek = DayOfWeek.Monday;

  constructor(
    private service: UserService,
    private tokenService: TokenStorageService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.service.getMyEvents().subscribe(
      events => {
        this.events = events;
      },
      ({ error }) => {
        this.error = error;
      }
    )
  }

  getFormattedDay(): string {
    return DayOfWeek[this.day];
  }

  formatProfessorName(event: IEvent): string {
    return [event.professor.lastName, event.professor.firstName, event.professor.middleName].filter(n => !!n).join(' ');
  }

  getEventsForDay(): IEvent[] | undefined {
    return this.events?.filter(event => event.dayOfWeek === this.day);
  }

  nextDay(): void {
    this.day++;
  }

  previousDay(): void {
    this.day--;
  }

  logout(): void {
    this.tokenService.logout();
    this.router.navigateByUrl('/');
  }
}
