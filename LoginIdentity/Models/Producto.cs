﻿namespace LoginIdentity.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Modelo { get; set; }
        public string? Descripcion { get; set; }
        public int Costo { get; set; }
        public int Stock { get; set; }
    }
}