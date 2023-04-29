using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planilla.Models
{
    public class permisos
    {
        public int accion { get; set; }
        public int id_detallepermisos { get; set; }
        public int id_colaborador { get; set; }
        public int id_tipopermiso { get; set; }

        public string tipo_permiso { get; set; }
        public int cantidad_horas { get; set; }
        public decimal monto_horas { get; set; }
        public int id_estadoplanilla { get; set; }
        public string estado_aplicacion_planilla { get; set; }

        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }

        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }

    }
}