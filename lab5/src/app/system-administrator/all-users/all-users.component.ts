import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import IUser from 'src/app/models/IUser';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.scss']
})
export class AllUsersComponent implements OnInit {
  users: IUser[] = [];
  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ users }) => {
      this.users = users;
    });
  }

}
