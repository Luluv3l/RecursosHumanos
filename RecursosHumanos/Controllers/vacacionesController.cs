using Planilla.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planilla.Controllers
{
    public class vacacionesController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";

        // GET: vacaciones
        public ActionResult Index()
        {
            return View();
        }

        // GET: vacaciones/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: vacaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vacaciones/Create
        [HttpPost]
        public ActionResult Create(int id_colaborador, DateTime fecha_i, DateTime fecha_f, vacaciones vacaciones)
        {
            //string msj = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {

                    SqlCommand cmd = new SqlCommand("sp_detalle_vacaciones_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", vacaciones.accion = 11);
                    cmd.Parameters.AddWithValue("@id_colaborador", vacaciones.id_colaborador = id_colaborador);
                    cmd.Parameters.Add("@fecha_inicial", SqlDbType.DateTime2).Value = fecha_i;
                    cmd.Parameters.Add("@fecha_final", SqlDbType.DateTime2).Value = fecha_f;


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



        // GET: vacaciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: vacaciones/Edit/5
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

        // GET: vacaciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: vacaciones/Delete/5
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

                string query = ("select id_colaborador, identificacion, nombre,apellido1 " +
                    "from colaboradores where identificacion = @identificacion");

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@identificacion", id);

                conn.Open();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                sd.Fill(ds);


                List<vacaciones> vacaciones = new List<vacaciones>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    vacaciones.Add(
                        new vacaciones
                        {
                            id_colaborador = Convert.ToInt32(dr["id_colaborador"]),
                            identificacion = Convert.ToString(dr["identificacion"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            apellido_1 = Convert.ToString(dr["apellido1"]),
                            //apellido_2 = Convert.ToString(dr["apellido2"]),
                            //correo = Convert.ToString(dr["correo_electronico"]),
                            //telefono = Convert.ToInt32(dr["telefono"]),
                            //puesto = Convert.ToInt32(dr["id_tipopuesto"].ToString()),
                            //planilla = Convert.ToInt32(dr["id_tipoplanilla"]),
                            //estado = Convert.ToInt32(dr["id_estadocolaborador"]),
                        });

                }

                conn.Close();

                return View("Create", vacaciones);
            }
        }



    }
}
