using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    class CDT : CuentaBancaria
    {
        private readonly int ConsignacionInicial = 1000000;
        public CDT()
        {

        }

        public override void Consignar(MovimientoFinanciero movimiento)
        {
            var movimientoBusquedad = this.Movimientos.Count();
            if (movimientoBusquedad!=0)
            {
                throw new InvalidOperationException("Solo se puede realizar una consignación");
            }
            if (movimiento.Monto>ConsignacionInicial) {
                base.Consignar(movimiento);
            }
            else
            {
                throw new InvalidOperationException("error");
            }
            
            
        }
        public override void Retirar(double valor)
        {
            throw new NotImplementedException();
        }
    }
}
