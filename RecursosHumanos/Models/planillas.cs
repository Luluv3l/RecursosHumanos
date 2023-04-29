using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planilla.Models
{
    public class planillas
    {
        public int accion { get; set; }
        public int id_colaborador { get; set; }

        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public int id_tipoplanilla { get; set; }
        public string planilla { get; set; }
        public int id_tipojornada { get; set; }
        public string jornada { get; set; }
        public int id_tipopuesto { get; set; }
        public string puesto { get; set; }

        public decimal salario_bruto { get; set; }
        public decimal monto_incapacidades { get; set; }
        public decimal monto_permisos { get; set; }
        public decimal monto_horas_extras { get; set; }
        public decimal monto_vacaciones_remuneradas { get; set; }
        public decimal deducciones_obligatorias { get; set; }
        public decimal impuestos_obligatorios { get; set; }
        public decimal salario_neto { get; set; }


    }
}