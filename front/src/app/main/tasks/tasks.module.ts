import { NgModule } from '@angular/core';
import { TasksComponent } from './tasks.component';
import {SharedModule} from "../../shared/shared.module";
import {RouterModule} from "@angular/router";
import {ModalModule} from "../../_modal";
import {StateDirective} from "./state.directive";



@NgModule({
  declarations: [
    TasksComponent,
    StateDirective
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
