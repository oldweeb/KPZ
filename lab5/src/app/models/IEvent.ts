import IGroup from "./IGroup";
import IUser from "./IUser";

interface IEvent {
  id: string;
  name: string;
  order: number;
  professor: IUser;
  group: IGroup;
  type: EventType;
  dayOfWeek: DayOfWeek;
}

export enum EventType {
  Lab,
  Lecture,
  Practice
}

export enum DayOfWeek {
  Sunday,
  Monday,
  Tuesday,
  Wednesday,
  Thursday,
  Friday,
  Saturday
}

export default IEvent;