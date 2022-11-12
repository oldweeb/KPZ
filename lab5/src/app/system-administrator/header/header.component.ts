import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from 'src/app/services/TokenStorageService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private tokenService: TokenStorageService, private router: Router) { }

  ngOnInit(): void {
  }

  logout(): void {
    this.tokenService.logout();
    this.router.navigateByUrl('login');
  }
}
