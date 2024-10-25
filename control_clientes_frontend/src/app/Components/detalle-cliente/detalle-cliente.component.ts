import { Component, Input } from '@angular/core';
import { ICliente } from 'src/app/Models/cliente.model';

@Component({
  selector: 'app-detalle-cliente',
  templateUrl: './detalle-cliente.component.html',
  styleUrls: ['./detalle-cliente.component.css']
})
export class DetalleClienteComponent {
  @Input() selectedClient: ICliente | null = null;

}
