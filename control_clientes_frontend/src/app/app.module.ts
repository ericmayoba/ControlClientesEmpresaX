import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HttpClientModule} from '@angular/common/http';
import { DetalleClienteComponent } from './Components/detalle-cliente/detalle-cliente.component';
import { CrearClienteComponent } from './Components/crear-cliente/crear-cliente.component'
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    DetalleClienteComponent,
    CrearClienteComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
