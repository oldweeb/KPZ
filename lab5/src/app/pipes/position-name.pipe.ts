import { Pipe, PipeTransform } from '@angular/core';
import { Position } from '../models/IUser';

@Pipe({
  name: 'positionName'
})
export class PositionNamePipe implements PipeTransform {

  transform(value: Position): string {
    return Position[value];
  }

}
