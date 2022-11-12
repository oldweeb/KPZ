import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import IEvent, { DayOfWeek } from '../models/IEvent';
import { TokenStorageService } from '../services/TokenStorageService';
import { UserService } from '../services/UserService';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html',
  styleUrls: ['./professor.component.scss'],
  providers: [UserService]
})
export class ProfessorComponent implements OnInit {
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

  nextDay(): void {
    this.day++;
    console.log(this.day);
  }

  previousDay(): void {
    this.day--;
    console.log(this.day);
  }

  eventsForDay(): IEvent[] | undefined {
    return this.events?.filter(e => e.dayOfWeek === this.day);
  }

  logout() {
    this.tokenService.logout();
    this.router.navigateByUrl('/');
  }
}
