using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planilla.Models
{
    public class colaboradores
    {
        public int accion { get; set; }
        public int id_colaborador { get; set; }
        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public DateTime fecha { get; set; }
        public int puesto { get; set; }
        public int planilla { get; set; }
        public int jornada { get; set; }
        public int estado { get; set; }


    }



}