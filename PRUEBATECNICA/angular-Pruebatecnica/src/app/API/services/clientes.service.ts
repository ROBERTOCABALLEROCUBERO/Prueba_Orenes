import { Injectable } from '@angular/core';
import axios from 'axios';
import { Cliente } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {
  private apiUrl = 'https://localhost:7191/api/Clientes';

  constructor() { }

  async login(username: string, password: string): Promise<string> {
    try {
      const response = await axios.post<{ token: string }>(`${this.apiUrl}/Login/?nombre=${username}&password=${password}`);
      return response.data.token;
    } catch (error) {
      throw new Error('Error en el proceso de login');
    }
  }
  
  async obtenerDatosUsuario(): Promise<Cliente> {
    try {
      const token = localStorage.getItem('token'); // Obtener el token del localStorage
      const headers = { Authorization: `Bearer ${token}` }; // Incluir el token en el encabezado Authorization

      const response = await axios.get<Cliente>(`${this.apiUrl}/ObtenerDatosUsuario`, { headers });
      return response.data;
    } catch (error) {
      throw 'Error al obtener los datos del usuario';
    }
  }

  async obtenerClientes(): Promise<Cliente[]> {
    try {
      const response = await axios.get<Cliente[]>(this.apiUrl);
      return response.data;
    } catch (error) {
      throw 'Error al obtener los clientes';
    }
  }

  async obtenerClientePorId(id: number): Promise<Cliente> {
    try {
      const response = await axios.get<Cliente>(`${this.apiUrl}/${id}`);
      return response.data;
    } catch (error) {
      throw  'Error al obtener el cliente';
    }
  }

  async actualizarCliente(id: number, cliente: Cliente): Promise<void> {
    try {
      await axios.put(`${this.apiUrl}/${id}`, cliente);
    } catch (error) {
      throw 'Error al actualizar el cliente';
    }
  }

  async registrarCliente(cliente: Cliente): Promise<number> {
    try {
      const response = await axios.post<{ clienteId: number }>(this.apiUrl, cliente);
      return response.data.clienteId;
    } catch (error) {
      throw 'Error en el proceso de registro';
    }
  }

  async eliminarCliente(id: number): Promise<void> {
    try {
      await axios.delete(`${this.apiUrl}/${id}`);
    } catch (error) {
      throw 'Error al eliminar el cliente';
    }
  }
}
