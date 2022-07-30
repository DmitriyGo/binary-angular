import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent } from './project.component';
import {SharedModule} from "../../shared/shared.module";
import {RouterModule} from "@angular/router";
import {ModalModule} from "../../_modal";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";



@NgModule({
  declarations: [
    ProjectComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      {path: 'projects', component: ProjectComponent}
    ]),
    ModalModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule
  ],
  exports: [
    RouterModule
  ]
})
export class ProjectsModule { }
