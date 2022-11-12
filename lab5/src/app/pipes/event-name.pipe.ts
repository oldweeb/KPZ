import { Pipe, PipeTransform } from '@angular/core';
import IEvent from '../models/IEvent';
import { UserNamePipe } from './user-name.pipe';

@Pipe({
  name: 'eventName'
})
export class EventNamePipe implements PipeTransform {
  constructor(private userNamePipe: UserNamePipe) { }

  transform(value: IEvent): string {
    return [value.name, this.userNamePipe.transform(value.professor), value.group.name].join(', ');
  }

}
