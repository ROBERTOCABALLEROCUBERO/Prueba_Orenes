import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  hasToken(): boolean {
    const token = localStorage.getItem('jwtToken');
    return !!token; // Devuelve true si hay un token, false si no lo hay
  }
}
