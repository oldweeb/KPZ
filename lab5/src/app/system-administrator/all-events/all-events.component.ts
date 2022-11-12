import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import IEvent from 'src/app/models/IEvent';

@Component({
  selector: 'app-all-events',
  templateUrl: './all-events.component.html',
  styleUrls: ['./all-events.component.scss']
})
export class AllEventsComponent implements OnInit {
  events: IEvent[] = [];
  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ events }) => {
      this.events = events;
    })
  }
}
