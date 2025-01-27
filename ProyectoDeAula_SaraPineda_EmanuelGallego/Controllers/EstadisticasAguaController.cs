﻿using ProyectoDeAula_SaraPineda_EmanuelGallego.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeAula_SaraPineda_EmanuelGallego.Controllers
{
    public class EstadisticasAguaController : Controller
    {
        private EpmEntities db = new EpmEntities();

        // GET: EstadisticasAgua
        public ActionResult Operaciones()
        {
            return View();
        }

        // Metodo para calcular la cantidad de m3 de agua consumidos por encima del promedio
        public ActionResult CantidadAguaMayorAlPromedio()
        {
            return View("operaciones");
        }

        // Metodo para calcular el porcentaje de consumo de agua excesivo de agua por estrato
        public ActionResult PorcentajeConsumoExcesivoPorEstrato()
        {
            return View("operaciones");
        }

        // Metodo para calcular el estrato con mayor consumo de agua
        public ActionResult EstratoConMayorAhorro()
        {
            return View("operaciones");
        }

        // Metodo para calcular cliente con mayor consumo de agua por periodo de consumo de agua
        public ActionResult ClienteMayorConsumoPorPeriodo()
        {
            return View("operaciones");
        }

        // Metodo para calcular el total a pagar a ep de tods los clientes
        public ActionResult TotalPagoEpm()
        {
            return View("operaciones");
        }

    }
}
