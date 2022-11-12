import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import IGroup from 'src/app/models/IGroup';
import { Position } from 'src/app/models/IUser';
import { INewUserDto, SystemAdministratorService } from 'src/app/services/SystemAdministratorService';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.scss']
})
export class NewUserComponent implements OnInit {
  availableGroups: IGroup[] = [];
  user: Partial<INewUserDto> = {};
  positions = Position;
  keys: string[] = [];
  constructor(private service: SystemAdministratorService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.keys = Object.keys(this.positions).filter(k => !Number.isNaN(+k));
  }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ availableGroups }) => {
      this.availableGroups = availableGroups;
    });
  }

  create(): void {
    if (this.user.position !== Position.Student) {
      this.user.groupId = undefined;
    }

    this.service.createUser(this.user as INewUserDto).subscribe(
      () => {
        alert('User successfully created.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`User creation failed. Description: ${error}`);
      }
    );
  }

  cancel(): void {
    this.router.navigateByUrl('admin');
  }
}
