using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planilla.Models
{
    public class vacaciones
    {
        public int accion { get; set; }
        public int id_colaborador { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }


        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }

    }
}