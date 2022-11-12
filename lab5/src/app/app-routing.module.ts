import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProfessorComponent } from './professor/professor.component';
import { StudentComponent } from './student/student.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'student', component: StudentComponent },
  { path: 'professor', component: ProfessorComponent },
  { path: 'admin', loadChildren: () => import('./system-administrator/system-administrator.module').then(m => m.SystemAdministratorModule) },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
