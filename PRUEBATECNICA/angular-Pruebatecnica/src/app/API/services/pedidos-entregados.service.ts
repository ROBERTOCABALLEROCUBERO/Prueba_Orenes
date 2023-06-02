
import { Injectable } from '@angular/core';
import axios from 'axios';
import { Pedido, PedidoDto } from '../models';
import { Observable, from } from 'rxjs';
import { AxiosResponse } from 'axios';

@Injectable({
  providedIn: 'root'
})

export class PedidosEntregadosService {
  private baseUrl = 'https://localhost:7191/api/PedidosEntregados'; // Reemplaza con la URL de tu backend

  obtenerPedidosPorCliente() {
    const token = localStorage.getItem('token'); // Obtén el token del localStorage

    // Configura los encabezados con el token de autenticación
    const headers = {
      Authorization: `Bearer ${token}`,
    };

    return axios.get(`${this.baseUrl}/obtenercliente`, { headers });
  }
}
