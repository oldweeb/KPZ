import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import IGroup from 'src/app/models/IGroup';

@Component({
  selector: 'app-all-groups',
  templateUrl: './all-groups.component.html',
  styleUrls: ['./all-groups.component.scss']
})
export class AllGroupsComponent implements OnInit {
  groups: IGroup[] = [];
  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ groups }) => {
      this.groups = groups;
    })
  }

}
