using Planilla.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Contexts;
using System.Reflection;
using System.Linq.Expressions;

namespace Planilla.Controllers
{
    public class permisosController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";

        // GET: permisos
        public ActionResult Index()
        {
            return View();
        }

        // GET: permisos/Details/5
        public ActionResult Details()
        {
            ModelState.Clear();
            return View(GetPermisos());
        }

        // GET: permisos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarValor2()
        {
            string opcionPermiso = Request.Form["opcionPermiso"];
            // Guardar el valor seleccionado en la base de datos


            return RedirectToAction("Create");
        }

        // POST: permisos/Create
        [HttpPost]
        public ActionResult Create(int id_colaborador, DateTime fecha_i, DateTime fecha_f, permisos permisos)
        {


            //string msj = "";
            try
            {

                string opcionPermiso = Request.Form["opcionPermiso"];


                using (SqlConnection conn = new SqlConnection(conexion))
                {

                    SqlCommand cmd = new SqlCommand("sp_detalle_permisos_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", permisos.accion = 11);
                    cmd.Parameters.AddWithValue("@id_colaborador", permisos.id_colaborador = id_colaborador);
                    cmd.Parameters.Add("@fecha_inicial", SqlDbType.DateTime2).Value = fecha_i;
                    cmd.Parameters.Add("@fecha_final", SqlDbType.DateTime2).Value = fecha_f;
                    cmd.Parameters.AddWithValue("@id_tipopermiso", opcionPermiso);


                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    TempData["SuccedMensaje"] = "SE CREADO CORRECTAMENTE EL REGISTRO...";

                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMensaje1"] = "ERROR!! AL CREAR EL NUEVO REGISTRO";
                TempData["ErrorMensaje"] = ex.Message;
            }
            return View();

        }


        // GET: permisos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: permisos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: permisos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: permisos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult buscar(string id)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = (" SELECT tb1.id_colaborador, tb1.identificacion, tb1.nombre, CONCAT(tb1.apellido1, ' ', tb1.apellido2) as apellidos\r\n\t\t FROM colaboradores tb1\r\n\t\t WHERE tb1.identificacion=@identificacion\r\n\t\t ORDER BY tb1.id_colaborador ASC");
                //string query = ("select id_colaborador, identificacion, nombre,apellido1 " +
                //    "from colaboradores where identificacion = @identificacion");

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@identificacion", id);

                conn.Open();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                sd.Fill(ds);


                List<permisos> permisos = new List<permisos>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    permisos.Add(
                        new permisos
                        {
                            id_colaborador = Convert.ToInt32(dr["id_colaborador"]),
                            identificacion = Convert.ToString(dr["identificacion"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            apellido_1 = Convert.ToString(dr["apellidos"]),
                            //apellido_2 = Convert.ToString(dr["apellido2"]),
                            //correo = Convert.ToString(dr["correo_electronico"]),
                            //telefono = Convert.ToInt32(dr["telefono"]),
                            //puesto = Convert.ToInt32(dr["id_tipopuesto"].ToString()),
                            //planilla = Convert.ToInt32(dr["id_tipoplanilla"]),
                            //estado = Convert.ToInt32(dr["id_estadocolaborador"]),
                        });

                }

                conn.Close();

                return View("Create", permisos);
            }
        }

        //Me lista mis registros
        public List<permisos> GetPermisos()
        {
            //try
            //{
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                List<permisos> permisos = new List<permisos>();

                string query = " SELECT tb1.id_detallepermisos, tb1.id_colaborador, \r\n\t\t\t\ttb2.identificacion, tb2.nombre, tb2.apellido1, tb2.apellido2,\r\n\t\t\t\ttb1.id_tipopermiso, tb3.descripcion AS tipo_permiso,\r\n\t\t\t\tCONVERT(DATE,tb1.fecha_inicial) AS fecha_inicial, CONVERT(DATE,tb1.fecha_final) AS fecha_final, \r\n\t\t\t\ttb1.dias_permiso AS cantidad_horas, FORMAT(tb1.monto_bruto_permiso,'N','en-US') AS monto_horas,\r\n\t\t\t\ttb1.id_estadoplanilla, tb4.descripcion AS estado_aplicacion_planilla\r\n\t\t FROM detalle_permisos_colaboradores tb1\r\n\t\t    INNER JOIN colaboradores tb2 ON tb1.id_colaborador=tb2.id_colaborador\r\n\t\t\tINNER JOIN tipos_permisos tb3 ON tb1.id_tipopermiso=tb3.id_tipopermisos\r\n\t\t\tINNER JOIN planillas_estados tb4 ON tb1.id_estadoplanilla=id_estado\r\n\t\t ORDER BY tb1.id_detallepermisos ASC ";
                SqlCommand cmd = new SqlCommand(query, conn);


                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                cmd.ExecuteNonQuery();
                sd.Fill(dt);
                conn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    permisos.Add(
                        new permisos
                        {
                            id_detallepermisos = Convert.ToInt32(dr["id_detallepermisos"]),
                            id_colaborador = Convert.ToInt32(dr["id_colaborador"]),

                            identificacion = Convert.ToString(dr["identificacion"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            apellido_1 = Convert.ToString(dr["apellido1"]),
                            apellido_2 = Convert.ToString(dr["apellido2"]),

                            id_tipopermiso = Convert.ToInt32(dr["id_tipopermiso"]),
                            tipo_permiso = Convert.ToString(dr["tipo_permiso"]),
                            fecha_inicio = Convert.ToDateTime(dr["fecha_inicial"]),
                            fecha_final = Convert.ToDateTime(dr["fecha_final"]),
                            cantidad_horas = Convert.ToInt32(dr["cantidad_horas"]),
                            monto_horas = Convert.ToDecimal(dr["monto_horas"]),
                            id_estadoplanilla = Convert.ToInt32(dr["id_estadoplanilla"]),
                            estado_aplicacion_planilla = Convert.ToString(dr["estado_aplicacion_planilla"])

                        }); ;
                }

                return permisos;

            }
            //}

            //catch(Exception ex)
            //{
            //    TempData["ErrorMensaje"] = ex.Message;
            //}

        }

        public ActionResult ReportDesignerP()
        {
            return View(); ;
        }
    }
}
