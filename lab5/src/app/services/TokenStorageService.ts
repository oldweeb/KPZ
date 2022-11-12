import { Injectable } from "@angular/core";
import { Position } from "../models/IUser";

const TOKEN_KEY = 'jwtToken';
const POSITION_KEY = 'position';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor() {
  }

  public logout() {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.removeItem(POSITION_KEY);
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  public getPosition(): Position | null {
    const position = window.sessionStorage.getItem(POSITION_KEY);
    return !!position ? parseInt(position) as Position : null;
  }

  public saveUser(token: string, position: Position) {
    window.sessionStorage.setItem(TOKEN_KEY, token);
    window.sessionStorage.setItem(POSITION_KEY, position.toString());
  }
}