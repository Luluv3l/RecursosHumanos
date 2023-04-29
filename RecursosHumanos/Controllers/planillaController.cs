using Planilla.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;

namespace Planilla.Controllers
{
    public class planillaController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";

        // GET: planilla
        public ActionResult Index()
        {
            return View();
        }

        // GET: planilla/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: planilla/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: planilla/Create


        [HttpPost]
        public ActionResult Create(DateTime date_i, DateTime date_f, int list, planillas planillas)
        {
            try
            {

                string opcionSeleccionada7 = Request.Form["opcionSeleccionada7"];
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_planillas", conn);
                    cmd.Parameters.AddWithValue("@accion", planillas.accion = 11);
                    cmd.Parameters.AddWithValue("@fecha_inicial", SqlDbType.DateTime2).Value = date_i;
                    cmd.Parameters.AddWithValue("@fecha_final", SqlDbType.DateTime2).Value = date_f;
                    cmd.Parameters.AddWithValue("@id_tipoplanilla", planillas).Value = list;

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

        // GET: planilla/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: planilla/Edit/5
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

        // GET: planilla/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: planilla/Delete/5
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
        public ActionResult buscar(DateTime fecha_i, DateTime fecha_f, planillas obp)
        {

            string opcionSeleccionada7 = Request.Form["opcionSeleccionada7"];
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_planillas", conn);
                cmd.Parameters.AddWithValue("@accion", obp.accion = 10);
                cmd.Parameters.AddWithValue("@fecha_inicial", SqlDbType.DateTime2).Value = fecha_i;
                cmd.Parameters.AddWithValue("@fecha_final", SqlDbType.DateTime2).Value = fecha_f;
                cmd.Parameters.AddWithValue("@id_tipoplanilla", opcionSeleccionada7);


                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                sd.Fill(ds);


                List<planillas> planillas = new List<planillas>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    planillas.Add(
                        new planillas
                        {
                            id_colaborador = Convert.ToInt32(dr["id_colaborador"]),
                            identificacion = Convert.ToString(dr["identificacion"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            apellido_1 = Convert.ToString(dr["apellido1"]),
                            apellido_2 = Convert.ToString(dr["apellido2"]),
                            fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                            id_tipoplanilla = Convert.ToInt32(dr["id_tipoplanilla"]),
                            planilla = Convert.ToString(dr["planilla"]),
                            id_tipojornada = Convert.ToInt32(dr["id_tipojornada"]),
                            jornada = Convert.ToString(dr["jornada"]),
                            id_tipopuesto = Convert.ToInt32(dr["id_tipopuesto"]),
                            puesto = Convert.ToString(dr["puesto"]),
                            salario_bruto = Convert.ToDecimal(dr["salario_bruto"]),
                            monto_incapacidades = Convert.ToDecimal(dr["monto_incapacidades"]),
                            monto_permisos = Convert.ToDecimal(dr["monto_permisos"]),
                            monto_horas_extras = Convert.ToDecimal(dr["monto_horas_extras"]),
                            monto_vacaciones_remuneradas = Convert.ToDecimal(dr["monto_vacaciones_remuneradas"]),
                            deducciones_obligatorias = Convert.ToDecimal(dr["deducciones_obligatorias"]),
                            impuestos_obligatorios = Convert.ToDecimal(dr["impuestos_obligatorios"]),
                            salario_neto = Convert.ToDecimal(dr["salario_neto"]),

                        });

                }

                conn.Close();

                return View("Create", planillas);
            }

        }

    }
}
