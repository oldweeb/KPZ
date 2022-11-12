import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import IEvent, { DayOfWeek, EventType } from "../models/IEvent";
import IGroup from "../models/IGroup";
import IUser, { Position } from "../models/IUser";

const API_URL = 'https://localhost:7223/';

export interface INewUserDto {
  firstName?: string;
  middleName?: string;
  lastName: string;
  email: string;
  groupId: string;
  position: Position;
  password: string;
}

export interface IEventDto {
  id?: string;
  order: number;
  professorId: string;
  groupId: string;
  type: EventType;
  name: string;
  dayOfWeek: DayOfWeek;
}

export interface IGroupDto {
  id?: string;
  name: string;
}

@Injectable()
export class SystemAdministratorService {
  constructor(private httpClient: HttpClient) {
  }

  createUser(newUser: INewUserDto): Observable<any> {
    return this.httpClient.post(API_URL + 'auth/new', newUser);
  }

  getAllUsers(): Observable<IUser[]> {
    return this.httpClient.get<IUser[]>(API_URL + 'users/all');
  }

  getEventById(id: string): Observable<IEvent> {
    return this.httpClient.get<IEvent>(API_URL + `events/${id}`);
  }

  getAllEvents(): Observable<IEvent[]> {
    return this.httpClient.get<IEvent[]>(API_URL + 'events/all')
  }

  createEvent(newEvent: IEventDto): Observable<any> {
    return this.httpClient.post(API_URL + 'events', newEvent);
  }

  updateEvent(event: IEventDto): Observable<any> {
    return this.httpClient.put(API_URL + 'events', event);
  }

  deleteEvent(id: string): Observable<any> {
    return this.httpClient.delete(API_URL + `events/${id}`);
  }

  getGroupById(id: string): Observable<IGroup> {
    return this.httpClient.get<IGroup>(API_URL + `groups/${id}`);
  }

  getAllGroups(): Observable<IGroup[]> {
    return this.httpClient.get<IGroup[]>(API_URL + 'groups/all');
  }

  createGroup(group: IGroupDto) {
    return this.httpClient.post(API_URL + 'groups', group);
  }

  updateGroup(group: IGroupDto): Observable<any> {
    return this.httpClient.put(API_URL + 'groups', group);
  }

  deleteGroup(id: string): Observable<any> {
    return this.httpClient.delete(API_URL + `groups/${id}`);
  }
}