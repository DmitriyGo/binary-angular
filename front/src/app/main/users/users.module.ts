import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users.component';
import {SharedModule} from "../../shared/shared.module";
import {RouterModule} from "@angular/router";
import {ModalModule} from "../../_modal";



@NgModule({
  declarations: [
    UsersComponent
  ],
    imports: [
        SharedModule,
        RouterModule.forChild([
            {path: 'users', component: UsersComponent}
        ]),
        ModalModule
    ],
  exports: [
    RouterModule
  ]
})
export class UsersModule { }
