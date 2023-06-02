import { Injectable } from '@angular/core';
import axios from 'axios';
import { Pedido, PedidoDto } from '../models';
import { Observable, from } from 'rxjs';
import { AxiosResponse } from 'axios';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  private baseUrl = 'https://localhost:7191/api/Pedidos'; // Reemplaza con la URL de tu backend

  obtenerPedidos() {
    return axios.get<Pedido[]>(`${this.baseUrl}`);
  }

  

  obtenerPedido(pedidoId: number) {
    return axios.get<Pedido>(`${this.baseUrl}/${pedidoId}`);
  }

  obtenerPedidosPorVehiculo(vehiculoId: number): Promise<AxiosResponse<PedidoDto[]>> {
    return axios.get<PedidoDto[]>(`${this.baseUrl}/PedidosVehiculo?vehiculoId=${vehiculoId}`);
  }

  crearPedido(pedido: PedidoDto) {
    const token = localStorage.getItem('token'); // Obtén el token del localStorage
    console.log(pedido);
    // Configura los encabezados con el token de autenticación y los tipos de contenido
    const headers = {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
    
    return axios.post<Pedido>(`${this.baseUrl}`, pedido, { headers });
  }
  obtenerPedidosCliente(token: string): Observable<PedidoDto[]> {
    const headers = { Authorization: `Bearer ${token}` };
    return from(
      axios.get<PedidoDto[]>(`${this.baseUrl}/PedidosCliente`, { headers }).then(response => response.data)
    );
  }

  marcarPedidoEnProceso(pedidoId: number, vehiculoId:number) {
    return axios.post<void>(`${this.baseUrl}/MarcarEnProceso?pedidoId=${pedidoId}&vehiculoId=${vehiculoId}`);
  }

  actualizarPedido(pedidoId: number, pedido: Pedido) {
    return axios.put<void>(`${this.baseUrl}/${pedidoId}`, pedido);
  }

  eliminarPedido(pedidoId: number) {
    return axios.delete<void>(`${this.baseUrl}/${pedidoId}`);
  }

  marcarPedidoComoEntregado(pedidoId: number) {
    return axios.post<void>(`${this.baseUrl}/MarcarEntregado?pedidoId=${pedidoId}`);
  }
}
