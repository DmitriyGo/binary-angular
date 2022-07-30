import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TasksComponent } from './tasks.component';
import {SharedModule} from "../../shared/shared.module";
import {RouterModule} from "@angular/router";
import {ModalModule} from "../../_modal";
import {ReactiveFormsModule} from "@angular/forms";



@NgModule({
  declarations: [
    TasksComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      {path: 'tasks', component: TasksComponent}
    ]),
    ModalModule,
  ],
  exports: [
    RouterModule
  ]
})
export class TasksModule { }
