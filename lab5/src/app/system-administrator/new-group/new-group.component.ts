import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IGroupDto, SystemAdministratorService } from 'src/app/services/SystemAdministratorService';

@Component({
  selector: 'app-new-group',
  templateUrl: './new-group.component.html',
  styleUrls: ['./new-group.component.scss']
})
export class NewGroupComponent implements OnInit {
  group: Partial<IGroupDto> = {};

  constructor(private service: SystemAdministratorService, private router: Router) { }

  ngOnInit(): void {
  }

  create(): void {
    this.service.createGroup(this.group as IGroupDto).subscribe(
      () => {
        alert('Group successfully created.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`Group creation failed. Description: ${error}`);
      }
    );
  }

  cancel(): void {
    this.router.navigateByUrl('admin');
  }
}
