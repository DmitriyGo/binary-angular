import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'ukrainianDate'})
export class UkrainianDatePipe implements PipeTransform {
  days: Array<String> = ["січня", "лютого", "березня", "квітня", "травня", "червня", "липня", "серпня", "вересня", "жовтня", "листопада", "грудня",]

  transform(date: Date): string {
    let d = new Date(date)
    let month = d.getMonth()
    return `${d.getDate()} ${this.days[month]} ${d.getFullYear()}`
  }
}
