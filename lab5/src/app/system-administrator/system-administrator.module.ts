import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SystemAdministratorRoutingModule } from './system-administrator-routing.module';
import { SystemAdministratorComponent } from './system-administrator.component';
import { NewUserComponent } from './new-user/new-user.component';
import { NewGroupComponent } from './new-group/new-group.component';
import { NewEventComponent } from './new-event/new-event.component';
import { EditGroupComponent } from './edit-group/edit-group.component';
import { EditEventComponent } from './edit-event/edit-event.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { AllGroupsComponent } from './all-groups/all-groups.component';
import { AllEventsComponent } from './all-events/all-events.component';
import { HeaderComponent } from './header/header.component';
import { FormsModule } from '@angular/forms';
import { UserNamePipe } from '../pipes/user-name.pipe';
import { DayNamePipe } from '../pipes/day-name.pipe';
import { TypeNamePipe } from '../pipes/type-name.pipe';
import { PositionNamePipe } from '../pipes/position-name.pipe';
import { EventNamePipe } from '../pipes/event-name.pipe';


@NgModule({
  declarations: [
    SystemAdministratorComponent,
    NewUserComponent,
    NewGroupComponent,
    NewEventComponent,
    EditGroupComponent,
    EditEventComponent,
    AllUsersComponent,
    AllGroupsComponent,
    AllEventsComponent,
    HeaderComponent,
    UserNamePipe,
    DayNamePipe,
    TypeNamePipe,
    PositionNamePipe,
    EventNamePipe
  ],
  imports: [
    CommonModule,
    SystemAdministratorRoutingModule,
    FormsModule,
  ]
})
export class SystemAdministratorModule { }
