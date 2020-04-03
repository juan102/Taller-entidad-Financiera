using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        public const double TOPERETIRO = 2000;

        public override void Consignar(MovimientoFinanciero movimiento)
        {
            var movimientoBusquedad = this.Movimientos.Where(T => T.Tipo == MovimientoType.INGRESO).Count();
            if(movimientoBusquedad==0 && movimiento.Monto >= 50000)
            {
                base.Consignar(movimiento);
            }else if (movimientoBusquedad != 0 && movimiento.Monto>0 && movimiento.TipoCiudad==CiudadType.Otros)
            {
                movimiento.Monto -= 10000;
                base.Consignar(movimiento);

            }
            else if (movimientoBusquedad != 0 && movimiento.Monto > 0 && movimiento.TipoCiudad == CiudadType.Origen)
            {
                base.Consignar(movimiento);
            }
        }
        public override void Retirar(double valor)
        {
            double nuevoSaldo = Saldo - valor;
            var movimiento = this.Movimientos.Where(T => T.Tipo == MovimientoType.EGRESO).Count();
            if (nuevoSaldo > TOPERETIRO && movimiento <= 3)
            {
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.Monto = valor;
                retiro.FechaMovimiento = DateTime.Now;
                retiro.Tipo = MovimientoType.EGRESO;
                Saldo -= valor;
                this.Movimientos.Add(retiro);
            }else if (nuevoSaldo > TOPERETIRO && movimiento >3)
            {
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.Monto = valor;
                retiro.FechaMovimiento = DateTime.Now;
                retiro.Tipo = MovimientoType.EGRESO;
                Saldo -= valor+5000;
                this.Movimientos.Add(retiro);
            }
            else
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
        }
    }
   

    [Serializable]
    public class CuentaAhorroTopeDeRetiroException : Exception
    {
        public CuentaAhorroTopeDeRetiroException() { }
        public CuentaAhorroTopeDeRetiroException(string message) : base(message) { }
        public CuentaAhorroTopeDeRetiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaAhorroTopeDeRetiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
