using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : CuentaBancaria
    {
        public const double SOBREGIRO = -1000;


        public override void Consignar(MovimientoFinanciero movimiento)
        {
            var movimientoBusuedad = this.Movimientos.Where(T => T.Tipo == MovimientoType.INGRESO).Count();
            if (movimientoBusuedad == 0 && movimiento.Monto > 100000)
            {
                base.Consignar(movimiento);
            }
            else if (movimientoBusuedad != 0 && movimiento.Monto > 0 )
            {
                
                base.Consignar(movimiento);

            }
        }

        public override void Retirar(double valor)
        {
            double nuevoSaldo = Saldo - valor;
            if (nuevoSaldo >= SOBREGIRO)
            {
                MovimientoFinanciero movimiento = new MovimientoFinanciero();
                movimiento.Monto = valor;
                movimiento.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(movimiento);
            }
            else
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar el Retiro, supera el valor de sobregiro permitido");
            }
        }
    }

   


    [Serializable]
    public class CuentaCorrienteRetirarMaximoSobregiroException : Exception
    {
        public CuentaCorrienteRetirarMaximoSobregiroException() { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message) : base(message) { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteRetirarMaximoSobregiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
