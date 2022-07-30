import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamsComponent } from './teams.component';
import {SharedModule} from "../../shared/shared.module";
import {RouterModule} from "@angular/router";
import {ModalModule} from "../../_modal";



@NgModule({
  declarations: [
    TeamsComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      {path: 'teams', component: TeamsComponent}
    ]),
    ModalModule
  ],
  exports: [
    RouterModule
  ]
})
export class TeamsModule { }
