import { Injectable } from '@angular/core';
import axios from 'axios';
import { Observable, from } from 'rxjs';
import { Vehiculo } from '../models/vehiculo';

@Injectable({
  providedIn: 'root'
})
export class VehiculoService {
  private baseUrl = 'https://localhost:7191'; // Reemplaza con la URL de tu API

  constructor() { }

  obtenerVehiculos(): Observable<Vehiculo[]> {
    const url = `${this.baseUrl}/api/Vehiculos`;
    return from(axios.get<Vehiculo[]>(url).then(response => response.data));
  }

  obtenerVehiculo(id: number): Observable<Vehiculo> {
    const url = `${this.baseUrl}/api/Vehiculos/${id}`;
    return from(axios.get<Vehiculo>(url).then(response => response.data));
  }

  actualizarVehiculo(id: number, vehiculo: Vehiculo): Observable<void> {
    const url = `${this.baseUrl}/api/Vehiculos/${id}`;
    return from(axios.put(url, vehiculo).then(() => {}));
  }

  agregarVehiculo(vehiculo: Vehiculo): Observable<Vehiculo> {
    const url = `${this.baseUrl}/api/Vehiculos`;
    return from(axios.post<Vehiculo>(url, vehiculo).then(response => response.data));
  }

  eliminarVehiculo(id: number): Observable<void> {
    const url = `${this.baseUrl}/api/Vehiculos/${id}`;
    return from(axios.delete(url).then(() => {}));
  }
}
