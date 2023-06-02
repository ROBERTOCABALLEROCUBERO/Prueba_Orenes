import { Pedido } from "./pedido";
import { EstadoPedido } from './estado-pedido';

export interface PedidoWithInfo extends Pedido {
  pedidoId?: number; 
  distancia?: number;
    tiempo?: { horas: number; minutos: number; segundos: number };
    status?: EstadoPedido;

  }