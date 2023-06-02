import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { VehiculoService } from '../API/services/vehiculos.service';
import { Vehiculo } from '../API/models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-acceso-vehiculo',
  templateUrl: './acceso-vehiculo.component.html',
  styleUrls: ['./acceso-vehiculo.component.css']
})
export class AccesoVehiculoComponent implements OnInit {
  vehiculoId: number = 0;

  constructor(private vehiculoService: VehiculoService, private router: Router) { }

  ngOnInit(): void {
  }

  buscarVehiculo(): void {
     // Convertir a cadena de texto
    this.vehiculoService.obtenerVehiculo(this.vehiculoId).subscribe(
      (vehiculo: Vehiculo) => {
        localStorage.setItem('vehiculoId', this.vehiculoId.toString());
        this.router.navigate(['/vehiculo']);
      },
      (error) => {
        console.error('Error al obtener el vehículo:', error);
        // Aquí puedes mostrar un mensaje de error al usuario si lo deseas
      }
    );
  }
}
