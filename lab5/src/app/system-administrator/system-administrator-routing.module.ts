import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SystemAdministratorService } from '../services/SystemAdministratorService';
import { AllEventsComponent } from './all-events/all-events.component';
import { AllGroupsComponent } from './all-groups/all-groups.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { AvailableGroupsResolver } from './resolvers/available-groups.resolver';
import { AvailableProfessorsResolver } from './resolvers/available-professors.resolver';
import { EditEventComponent } from './edit-event/edit-event.component';
import { EditGroupComponent } from './edit-group/edit-group.component';
import { NewEventComponent } from './new-event/new-event.component';
import { NewGroupComponent } from './new-group/new-group.component';
import { NewUserComponent } from './new-user/new-user.component';
import { SystemAdministratorComponent } from './system-administrator.component';
import { AvailableUsersResolver } from './resolvers/available-users.resolver';
import { AvailableEventsResolver } from './resolvers/available-events.resolver';
import { UserNamePipe } from '../pipes/user-name.pipe';

const routes: Routes = [
  { path: '', component: SystemAdministratorComponent, pathMatch: 'full' },
  {
    path: 'new-user',
    component: NewUserComponent,
    resolve: {
      availableGroups: AvailableGroupsResolver
    }
  },
  { path: 'new-group', component: NewGroupComponent },
  {
    path: 'new-event',
    component: NewEventComponent,
    resolve: {
      availableGroups: AvailableGroupsResolver,
      availableProfessors: AvailableProfessorsResolver
    }
  },
  {
    path: 'edit-group',
    component: EditGroupComponent,
    resolve: {
      groups: AvailableGroupsResolver
    }
  },
  {
    path: 'edit-event',
    component: EditEventComponent,
    resolve: {
      events: AvailableEventsResolver,
      availableProfessors: AvailableProfessorsResolver,
      availableGroups: AvailableGroupsResolver
    }
  },
  {
    path: 'all-users',
    component: AllUsersComponent,
    resolve: {
      users: AvailableUsersResolver
    }
  },
  {
    path: 'all-groups',
    component: AllGroupsComponent,
    resolve: {
      groups: AvailableGroupsResolver
    }
  },
  {
    path: 'all-events',
    component: AllEventsComponent,
    resolve: {
      events: AvailableEventsResolver
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [
    SystemAdministratorService,
    AvailableGroupsResolver,
    AvailableProfessorsResolver,
    AvailableUsersResolver,
    AvailableEventsResolver,
    UserNamePipe
  ]
})
export class SystemAdministratorRoutingModule { }
