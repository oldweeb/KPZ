import { Pipe, PipeTransform } from '@angular/core';
import IUser from '../models/IUser';

@Pipe({
  name: 'userName'
})
export class UserNamePipe implements PipeTransform {

  transform(value: IUser): string {
    return [value.lastName, value.firstName, value.middleName].filter(n => !!n).join(' ');
  }

}
