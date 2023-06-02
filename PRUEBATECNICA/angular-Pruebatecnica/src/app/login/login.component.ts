import { Component } from '@angular/core';
import { ClientesService } from '../API/services';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
 
  constructor(private clientesService: ClientesService, private router: Router) {}
  username: string = '';
  password: string = '';
  async login(): Promise<void> {
  
    try {
      console.log(this.username, this.password);
      const token = await this.clientesService.login(this.username, this.password);
      localStorage.setItem('token', token);
      // Redirige a la ruta /cliente
      // Asegúrate de importar Router desde '@angular/router'
      this.router.navigate(['/cliente']);
    } catch (error) {
      console.error(error);
      // Manejo de errores o mostrar mensaje de error
    }
  }
  
}
