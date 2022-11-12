import { Pipe, PipeTransform } from '@angular/core';
import { DayOfWeek } from '../models/IEvent';

@Pipe({
  name: 'dayName'
})
export class DayNamePipe implements PipeTransform {

  transform(value: DayOfWeek): string {
    return DayOfWeek[value];
  }

}
