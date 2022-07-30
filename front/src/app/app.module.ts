import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {SharedModule} from "./shared/shared.module";
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomePageComponent } from './home-page/home-page.component';

import {TasksModule} from "./main/tasks/tasks.module";
import {ProjectsModule} from "./main/projects/projects.module";
import {UsersModule} from "./main/users/users.module";
import {TeamsModule} from "./main/teams/teams.module";
import {ModalModule} from "./_modal";
import {FormsModule} from "@angular/forms";
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    ModalModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ProjectsModule,
    TasksModule,
    UsersModule,
    TeamsModule,
    SharedModule,
    NoopAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule {}
