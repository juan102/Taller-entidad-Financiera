using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MovimientoFinanciero : Entity<int>
    {
      //  public CuentaBancaria CuentaBancaria { get; set; }
        public double Monto { get; set; }
        public MovimientoType Tipo { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public CiudadType TipoCiudad { get; set; }

       
       
    }

    public enum MovimientoType
    {
        INGRESO,
        EGRESO
    }
    public enum CiudadType
    {
        Origen,
        Otros
    }
}
