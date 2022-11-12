import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { StudentComponent } from './student/student.component';
import { ProfessorComponent } from './professor/professor.component';
import { authInterceptorProviders } from './helpers/AuthorizationInteceptor';
import { contentTypeInterceptorProviders } from './helpers/ContentTypeInterceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    StudentComponent,
    ProfessorComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    ...authInterceptorProviders,
    ...contentTypeInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
