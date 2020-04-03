using Domain.Entities;
using NUnit.Framework;
using System;

namespace Domain.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignacionAhorroTest()
        {
            CuentaAhorro cuenta = new CuentaAhorro();
            MovimientoFinanciero movimiento = new MovimientoFinanciero()
            {
                Monto = 0,
                TipoCiudad = CiudadType.Origen
            };

            cuenta.Consignar(movimiento);
            Assert.AreEqual(0,cuenta.Saldo);
        }

        [Test]
        public void ConsignacionAhorro2Test(){

            CuentaAhorro cuenta = new CuentaAhorro();
            MovimientoFinanciero movimiento = new MovimientoFinanciero()
            {
                Monto = 50000,
                TipoCiudad = CiudadType.Origen
            };

            cuenta.Consignar(movimiento);
            Assert.AreEqual(50000, cuenta.Saldo);

        }
        [Test]
        public void ConsignacionTarjetaTest()
        { 

            TarjetaCredito Tarjeta = new TarjetaCredito();
            Tarjeta.Saldo = 100000;
            MovimientoFinanciero movimiento = new MovimientoFinanciero()
            {
                Monto = 50000,
                TipoCiudad = CiudadType.Origen
            };
            Tarjeta.Consignar(movimiento);
            Assert.AreEqual(150000, Tarjeta.Saldo);
        }


    }
}