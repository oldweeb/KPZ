import { Pipe, PipeTransform } from '@angular/core';
import { EventType } from '../models/IEvent';

@Pipe({
  name: 'typeName'
})
export class TypeNamePipe implements PipeTransform {

  transform(value: EventType): string {
    return EventType[value];
  }

}
