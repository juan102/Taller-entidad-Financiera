using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class TarjetaCredito : CuentaBancaria
    {


        public TarjetaCredito()
        {

        }


        public override void Consignar(MovimientoFinanciero movimiento)
        {
            if (movimiento.Monto<=0)
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("el valor no puede ser 0");
            }
            if (movimiento.Monto>Saldo)
            {
                throw new InvalidOperationException("El valor  no puede ser mayor al saldo ");
            }
            base.Consignar(movimiento);

        }

        public override void Retirar(double valor)
        {
            if (valor <= 0)
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("el valor debe ser mayor");
            }
            if (valor > Saldo)
            {
                throw new InvalidOperationException("El valor  no puede ser mayor al saldo");
            }
            MovimientoFinanciero retiro = new MovimientoFinanciero();
            retiro.Monto = valor;
            retiro.FechaMovimiento = DateTime.Now;
            retiro.Tipo = MovimientoType.EGRESO;
            Saldo -= valor;
            this.Movimientos.Add(retiro);
        }
    }
}
