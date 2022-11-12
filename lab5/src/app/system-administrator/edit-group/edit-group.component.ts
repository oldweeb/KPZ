import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import IGroup from 'src/app/models/IGroup';
import { IGroupDto, SystemAdministratorService } from 'src/app/services/SystemAdministratorService';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.scss']
})
export class EditGroupComponent implements OnInit {
  groups: IGroup[] = [];
  group: Partial<IGroupDto> = {};
  selectedId: string | null = null;
  constructor(private service: SystemAdministratorService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.data.subscribe(({ groups }) => {
      this.groups = groups;
    })
  }

  update(): void {
    this.service.updateGroup(this.group as IGroupDto).subscribe(
      () => {
        alert('Group has been successfully updated.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`Group updating failed. Description: ${error}`);
      }
    )
  }

  onChangeGroup(event: any): void {
    this.selectedId = event.target.selectedOptions[0].value;
    const group = this.groups.filter(g => g.id === this.selectedId)[0];
    this.group.name = group.name;
    this.group.id = group.id;
  }

  cancel(): void {
    this.router.navigateByUrl('admin');
  }

  delete(): void {
    this.service.deleteGroup(this.group.id!).subscribe(
      () => {
        alert('Group has been successfully deleted.');
        this.router.navigateByUrl('admin');
      },
      ({ error }) => {
        alert(`Group deletion failed. Description: ${error}`);
      }
    )
  }
}
