export interface ICliente {
  ClienteId:   number;
  Nombre:      string;
  Email:       string;
  Telefono:    string;
  OtrosDatos:  string;
  Direcciones: IDirecciones[];
}

export interface IDirecciones {
  DireccionId: number;
  ClienteId:   number;
  Calle:       string;
  Sector:      string;
  Provincia:   string;
  Pais:        string;
}
