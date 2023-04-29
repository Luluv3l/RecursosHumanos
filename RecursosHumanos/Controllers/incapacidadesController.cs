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
    public class incapacidadesController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";

        public ActionResult Index()
        {
            return View();
        }

        // GET: incapacidades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: incapacidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: incapacidades/Create
        [HttpPost]
        public ActionResult Create(int id_colaborador, DateTime fecha_i, DateTime fecha_f, incapacidades incapacidades)
        {

            //string msj = "";
            try
            {

                string opcionIncapacidad = Request.Form["opcionIncapacidad"];


                using (SqlConnection conn = new SqlConnection(conexion))
                {

                    SqlCommand cmd = new SqlCommand("sp_detalle_incapacidades_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", incapacidades.accion = 11);
                    cmd.Parameters.AddWithValue("@id_colaborador", incapacidades.id_colaborador = id_colaborador);
                    cmd.Parameters.Add("@fecha_inicial", SqlDbType.DateTime2).Value = fecha_i;
                    cmd.Parameters.Add("@fecha_final", SqlDbType.DateTime2).Value = fecha_f;
                    cmd.Parameters.AddWithValue("@id_tipoincapacidad", opcionIncapacidad);


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


        // GET: incapacidades/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: incapacidades/Edit/5
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

        // GET: incapacidades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: incapacidades/Delete/5
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


                List<incapacidades> incapacidades = new List<incapacidades>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    incapacidades.Add(
                        new incapacidades
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

                return View("Create", incapacidades);
            }
        }


    }
}
