using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public abstract class CuentaBancaria : Entity<int>, IServicioFinanciero
    {
        public CuentaBancaria()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public double Saldo { get;  set; }
        public virtual void Consignar(MovimientoFinanciero movimiento)
        {
            movimiento.FechaMovimiento = DateTime.Now;
            movimiento.Tipo = MovimientoType.INGRESO;
            Movimientos.Add(movimiento);
            Saldo +=movimiento.Monto;
           
        }

       

        public abstract void Retirar(double valor);

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, MovimientoFinanciero movimiento)
        {
            Retirar(movimiento.Monto);
            servicioFinanciero.Consignar( movimiento); 
        }
    }

    
}
