import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {UkrainianDatePipe} from "./UkrainianDate.pipe";
import {ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    UkrainianDatePipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
    // RouterModule.forChild([
    //   {path: '**', component: PageNotFoundComponent}
    // ])
  ],
  exports: [
    RouterModule,
    CommonModule,
    UkrainianDatePipe,
    ReactiveFormsModule,
  ]


})
export class SharedModule { }
