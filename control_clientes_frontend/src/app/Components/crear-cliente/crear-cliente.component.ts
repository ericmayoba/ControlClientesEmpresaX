import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { ICliente } from 'src/app/Models/cliente.model';


@Component({
  selector: 'app-crear-cliente',
  templateUrl: './crear-cliente.component.html',
  styleUrls: ['./crear-cliente.component.css']
})
export class CrearClienteComponent implements OnInit {

  clienteForm: FormGroup;
  @Output() clienteGuardado = new EventEmitter<void>();

  constructor(private fb: FormBuilder, private _apiService: ApiService) {
    this.clienteForm = this.fb.group({
      nombre: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefono: ['', Validators.required],
      otrosDatos: ['', Validators.required],
      direcciones: this.fb.array([]) // FormArray para múltiples direcciones
    });
  }

  ngOnInit(): void {
    // Iniciar con una dirección vacía
    this.addDireccion();
  }

  // Getter para acceder al FormArray de direcciones
  get direcciones(): FormArray {
    return this.clienteForm.get('direcciones') as FormArray;
  }

  // Método para agregar una nueva dirección
  addDireccion() {
    const direccionForm = this.fb.group({
      calle: ['', Validators.required],
      sector: ['', Validators.required],
      provincia: ['', Validators.required],
      pais: ['', Validators.required]
    });
    this.direcciones.push(direccionForm);
  }

  // Método para eliminar una dirección
  removeDireccion(index: number) {
    this.direcciones.removeAt(index);
  }

  // Método para guardar el cliente con direcciones
  saveCliente() {
    if (this.clienteForm.valid) {
      const nuevoCliente: ICliente = {
        ClienteId: 0, // Asigna un valor por defecto o quítalo si no es necesario
        Nombre: this.clienteForm.value.nombre,
        Email: this.clienteForm.value.email,
        Telefono: this.clienteForm.value.telefono,
        OtrosDatos: this.clienteForm.value.otrosDatos,
        Direcciones: this.clienteForm.value.direcciones // Esto ya es un array de direcciones
      };

      this._apiService.postClient(nuevoCliente).subscribe({
        next: (data) => {
          this.clienteGuardado.emit();
        },
        error: (error) => {
          console.error('Error al crear el cliente:', error);
        }
      });
    } else {
      console.log('Formulario no válido');
    }
  }

}
