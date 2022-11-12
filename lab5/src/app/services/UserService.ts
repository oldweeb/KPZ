import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import IEvent from "../models/IEvent";

const API_URL = 'https://localhost:7223/';

@Injectable()
export class UserService {
  constructor(private httpClient: HttpClient) {
  }

  getMyEvents(): Observable<IEvent[]> {
    return this.httpClient.get<IEvent[]>(API_URL + 'events/my', { responseType: 'json' });
  }
}