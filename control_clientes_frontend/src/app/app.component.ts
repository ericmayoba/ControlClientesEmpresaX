import { Component, OnInit } from '@angular/core';
import { ICliente } from './Models/cliente.model';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  clientList: ICliente[] = [];
  selectedClient: ICliente | null = null;
  showCrearClienteModal: boolean = false;

  constructor(private _apiService: ApiService) {}

  ngOnInit(): void {
      this._apiService.getAllClients().subscribe((data: ICliente[]) =>{
        this.clientList = data;
      })
  }

  deleteClient(clienteId: number): void {
    this._apiService.deleteClient(clienteId).subscribe({
      next: () => {
        console.log('Cliente eliminado:', clienteId);
        // Actualizar la lista de clientes después de eliminar
        this.ngOnInit();
      },
      error: (error) => {
        console.error('Error al eliminar el cliente:', error);
      }
    });
  }

  selectClient(client: ICliente) {
    this.selectedClient = client;
  }

  closeModal(){
    this.selectedClient = null;
  }

  // Método para abrir el modal
  openCrearClienteModal() {
    this.showCrearClienteModal = true;
  }

  // Método para cerrar el modal
  closeCrearClienteModal() {
    this.showCrearClienteModal = false;
  }

  onClienteGuardado() {
    this.closeCrearClienteModal();
    this.ngOnInit();
  }
}
