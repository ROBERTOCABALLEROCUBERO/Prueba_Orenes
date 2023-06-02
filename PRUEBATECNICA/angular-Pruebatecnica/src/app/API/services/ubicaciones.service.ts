import { Injectable } from '@angular/core';
import axios from 'axios';
import { Ubicacion } from '../models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UbicacionService {
  private baseUrl = 'https://localhost:7191/api/ubicaciones'; // Reemplaza con la URL de tu backend

  obtenerUbicaciones() {
    return axios.get<Ubicacion[]>(`${this.baseUrl}`);
  }

  obtenerUbicacion(ubicacionId: number) {
    return axios.get<Ubicacion>(`${this.baseUrl}/${ubicacionId}`);
  }

  actualizarUbicacion(ubicacionId: number, ubicacion: Ubicacion) {
    return axios.put<void>(`${this.baseUrl}/${ubicacionId}`, ubicacion);
  }

  agregarUbicacion(ubicacion: Ubicacion) {
    return axios.post<Ubicacion>(`${this.baseUrl}`, ubicacion);
  }

  iniciarSeguimientoUbicacion(pedidoId: number, vehiculoId: number, lon:number, lat:number) {
    const ubicacion: Ubicacion = {
      ubicacionId: 0,// Puedes asignar un valor temporal o dejarlo en cero si se generará automáticamente en el servidor
      vehiculoId: vehiculoId,
      pedidoId: pedidoId,
      latitud: lat, // Ajusta esto con la latitud real
      longitud: lon, // Ajusta esto con la longitud real
      fechaHora: new Date() // Usa la fecha y hora actual
    };
  
    return axios.post<Ubicacion>(`${this.baseUrl}`, ubicacion);
  }
  
  eliminarUbicacion(ubicacionId: number) {
    return axios.delete<void>(`${this.baseUrl}/${ubicacionId}`);
  }

  obtenerUbicacionMasReciente(pedidoId: number): Observable<Ubicacion> {
    const url = `${this.baseUrl}/pedidos?pedidoId=${pedidoId}`;
    return new Observable<Ubicacion>((observer) => {
      axios
        .get<Ubicacion>(url)
        .then((response) => {
          observer.next(response.data);
          observer.complete();
        })
        .catch((error) => {
          observer.error(error);
        });
    });
  }
}
