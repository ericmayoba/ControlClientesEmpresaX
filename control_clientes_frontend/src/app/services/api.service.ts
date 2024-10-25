import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICliente } from '../Models/cliente.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseURL ='http://localhost:5136/api/Clientes'

  constructor(private _httpClient: HttpClient) { }

    public getAllClients(): Observable<ICliente[]>{
      return this._httpClient.get<ICliente[]>(this.baseURL);
    }

    public getClient(id: number): Observable<ICliente>{
      return this._httpClient.get<ICliente>(`${this.baseURL}/${id}`);
    }

    public postClient(cliente: ICliente): Observable<ICliente>{
      return this._httpClient.post<ICliente>(`${this.baseURL}`,  cliente);
    }

    public putClient(id: number, cliente: ICliente): Observable<ICliente>{
      return this._httpClient.put<ICliente>(`${this.baseURL}/${id}`, cliente);
    }

    public deleteClient(id: number): Observable<ICliente>{
      return this._httpClient.delete<ICliente>(`${this.baseURL}/${id}`);
    }
}
