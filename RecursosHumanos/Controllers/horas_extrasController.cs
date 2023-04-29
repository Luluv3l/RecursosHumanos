using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planilla.Models;

namespace Planilla.Controllers
{
    public class horas_extrasController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";

        // GET: horas_extras
        public ActionResult Index()
        {
            return View();
        }

        // GET: horas_extras/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: horas_extras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: horas_extras/Create
        [HttpPost]
        public ActionResult Create(int id_colaborador, DateTime fecha_i, horas_extras horas_)
        {


            try
            {

                string tipohora = Request.Form["tipohora"];
                string cantdiadhora = Request.Form["cantdiadhora"];

                using (SqlConnection conn = new SqlConnection(conexion))
                {

                    SqlCommand cmd = new SqlCommand("sp_detalle_horasextras_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", horas_.accion = 11);
                    //cmd.Parameters.AddWithValue("@id_permiso", permisos.id_permiso);
                    cmd.Parameters.AddWithValue("@id_colaborador", horas_.id_colaborador = id_colaborador);
                    cmd.Parameters.Add("@fecha_inicial", SqlDbType.DateTime2).Value = fecha_i;
                    //cmd.Parameters.AddWithValue("@fecha_inicial", permisos.fecha_inicio);
                    //cmd.Parameters.Add("@fecha_final", SqlDbType.DateTime2).Value = fecha_f;
                    //cmd.Parameters.AddWithValue("@fecha_final", permisos.fecha_final);
                    cmd.Parameters.AddWithValue("@id_tipohoraextra", tipohora);
                    cmd.Parameters.AddWithValue("@cantidad_horaextras", cantdiadhora);
                    //cmd.Parameters.AddWithValue("@estado", permisos.estado);

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



        // GET: horas_extras/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: horas_extras/Edit/5
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

        // GET: horas_extras/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: horas_extras/Delete/5
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


                List<horas_extras> horas_extras = new List<horas_extras>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    horas_extras.Add(
                        new horas_extras
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

                return View("Create", horas_extras);
            }
        }

    }
}
