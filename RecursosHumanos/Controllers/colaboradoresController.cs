using Planilla.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Data.OleDb;

namespace Planilla.Controllers
{
    public class colaboradoresController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";
        // GET: colaboradores
        // GET: deducciones
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarValor()
        {
            string opcionSeleccionada = Request.Form["opcionSeleccionada"];
            string opcionSeleccionada1 = Request.Form["opcionSeleccionada1"];
            string opcionSeleccionada2 = Request.Form["opcionSeleccionada2"];


            // Guardar el valor seleccionado en la base de datos
            return RedirectToAction("Create");
        }



        //public List<colaboradores> GetColaboradores()
        //{
        //    using (SqlConnection conn = new SqlConnection(conexion))
        //    {
        //        List<colaboradores> colaboradores = new List<colaboradores>();

        //        SqlCommand cmd = new SqlCommand("sp_obtener_lista", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter sd = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        sd.Fill(dt);
        //        conn.Close();

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            colaboradores.Add(
        //                new colaboradores
        //                {
        //                    id_colaborador = Convert.ToInt32(dr["id_colaborador"]),
        //                    nombre = Convert.ToString(dr["nombre"]),
        //                    apellido_1 = Convert.ToString(dr["apellido_1"]),
        //                    apellido_2 = Convert.ToString(dr["apellido_2"]),
        //                    correo = Convert.ToString(dr["correo_electronico"]),
        //                    telefono = Convert.ToInt32(dr["telefono"]),
        //                    estado = Convert.ToInt32(dr["tipo_estado"]),

        //                });
        //        }
        //        return colaboradores;
        //    }
        //}

        // GET: colaboradores/Details/5
        public ActionResult Details()
        {
            return View();

        }

        // GET: colaboradores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: colaboradores/Create
        [HttpPost]
        public ActionResult Create(colaboradores colaboradores)
        {

            try
            {
                string opcionSeleccionada = Request.Form["opcionSeleccionada"];
                string opcionSeleccionada1 = Request.Form["opcionSeleccionada1"];
                string opcionSeleccionada2 = Request.Form["opcionSeleccionada2"];
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", colaboradores.accion = 11);
                    //cmd.Parameters.AddWithValue("@id", colaboradores.id_colaborador);
                    cmd.Parameters.AddWithValue("@identificacion", colaboradores.identificacion);
                    cmd.Parameters.AddWithValue("@nombre", colaboradores.nombre);
                    cmd.Parameters.AddWithValue("@apellido1", colaboradores.apellido_1);
                    cmd.Parameters.AddWithValue("@apellido2", colaboradores.apellido_2);
                    cmd.Parameters.AddWithValue("@correelectronico", colaboradores.correo);
                    cmd.Parameters.AddWithValue("@telefono", colaboradores.telefono);
                    cmd.Parameters.AddWithValue("@fechaingreso", colaboradores.fecha);
                    cmd.Parameters.AddWithValue("@id_tipopuesto", opcionSeleccionada);
                    cmd.Parameters.AddWithValue("@id_tipoplanilla", opcionSeleccionada1);
                    cmd.Parameters.AddWithValue("@id_tipojornada", opcionSeleccionada2);

                    cmd.Parameters.AddWithValue("@id_estadocolaborador", colaboradores.estado=0);
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




        //// GET: colaboradores/Edit/5
        public ActionResult Edit(int id)
        {
            colaboradores colaboradores = new colaboradores();
            DataTable dT = new DataTable();
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                string query = " select id_colaborador, identificacion, nombre, apellido1, apellido2, correo_electronico,telefono,fecha_ingreso,id_tipopuesto,id_tipoplanilla,id_tipojornada,id_estadocolaborador " +
                    "from colaboradores" +
                    " where id_colaborador = " + id;
                //string query = "Select * from colaboradores where id_colaborador = " + id;

                SqlDataAdapter sqlData = new SqlDataAdapter(query, conn);

                sqlData.Fill(dT);
            }
            if (dT.Rows.Count == 1)
            {
                colaboradores.id_colaborador = Convert.ToInt32(dT.Rows[0][0]);
                colaboradores.identificacion = Convert.ToString(dT.Rows[0][1]);
                colaboradores.nombre = dT.Rows[0][2].ToString();
                colaboradores.apellido_1 = dT.Rows[0][3].ToString();
                colaboradores.apellido_2 = dT.Rows[0][4].ToString();
                colaboradores.correo = dT.Rows[0][5].ToString();
                colaboradores.telefono = Convert.ToInt32(dT.Rows[0][6]);
                colaboradores.fecha = Convert.ToDateTime(dT.Rows[0][7].ToString());
                colaboradores.puesto = Convert.ToInt32(dT.Rows[0][8]);
                colaboradores.planilla = Convert.ToInt32(dT.Rows[0][9]);
                colaboradores.jornada = Convert.ToInt32(dT.Rows[0][10]);
                colaboradores.estado = Convert.ToInt32(dT.Rows[0][11]);
                return View(colaboradores);
            }

            return RedirectToAction("Details");

        }

        //// POST: colaboradores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, colaboradores colaboradores)
        {
            try
            {

                //DateTime fecha_i = new DateTime();
                //string fechaStr = fecha_i.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //string opcionSeleccionada = Request.Form["opcionSeleccionada"];
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", colaboradores.accion = 12);
                    cmd.Parameters.AddWithValue("@id", colaboradores.id_colaborador);
                    cmd.Parameters.AddWithValue("@identificacion", colaboradores.identificacion);
                    cmd.Parameters.AddWithValue("@nombre", colaboradores.nombre);
                    cmd.Parameters.AddWithValue("@apellido1", colaboradores.apellido_1);
                    cmd.Parameters.AddWithValue("@apellido2", colaboradores.apellido_2);
                    cmd.Parameters.AddWithValue("@correelectronico", colaboradores.correo);
                    cmd.Parameters.AddWithValue("@telefono", colaboradores.telefono);
                    cmd.Parameters.AddWithValue("@fechaingreso", colaboradores.fecha);

                    cmd.Parameters.AddWithValue("@id_tipopuesto", colaboradores.puesto);
                    cmd.Parameters.AddWithValue("@id_tipoplanilla", colaboradores.planilla);
                    cmd.Parameters.AddWithValue("@id_tipojornada", colaboradores.jornada);
                    cmd.Parameters.AddWithValue("@id_estadocolaborador", colaboradores.estado);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                    TempData["SuccedMensaje"] = "SE A MODIFICANDO CORRECTAMENTE EL REGISTRO...";

                }

                return RedirectToAction("Details");

            }
            catch (Exception ex)
            {
                TempData["ErrorMensaje1"] = "ERROR!! AL CREAR EL NUEVO REGISTRO";
                TempData["ErrorMensaje"] = ex.Message;
                return View();
            }
        }



        // POST: colaboradores/Delete/5
        public ActionResult Delete(int id, colaboradores colaboradores)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_colaboradores", conn);
                    cmd.Parameters.AddWithValue("@accion", colaboradores.accion = 13);
                    cmd.Parameters.AddWithValue("@id", id);
                    //cmd.Parameters.AddWithValue("@id_estadocolaborador", colaboradores.estado);        
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    TempData["SuccedMensaje"] = "SE A ELIMINADO EL REGISTRO DE FORMA EXITOSA!!...";
                }

            }
            catch (Exception ex)

            {
                TempData["ErrorMensaje1"] = "ERROR!! AL ELMINAR EL REGISTRO";
                TempData["ErrorMensaje"] = ex.Message;
            }
            return RedirectToAction("Details");
        }



        [HttpPost]
        public ActionResult Buscar(string id)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {


                string query = ("select id_colaborador, identificacion, nombre,apellido1,apellido2,correo_electronico,telefono,id_tipopuesto,id_tipoplanilla,id_estadocolaborador " +
                    "from colaboradores where identificacion = @identificacion");

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@identificacion", id);

                conn.Open();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                sd.Fill(ds);


                List<colaboradores> colaboradores = new List<colaboradores>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    colaboradores.Add(
                        new colaboradores
                        {
                            id_colaborador = Convert.ToInt32(dr["id_colaborador"]),
                            identificacion = Convert.ToString(dr["identificacion"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            apellido_1 = Convert.ToString(dr["apellido1"]),
                            apellido_2 = Convert.ToString(dr["apellido2"]),
                            correo = Convert.ToString(dr["correo_electronico"]),
                            telefono = Convert.ToInt32(dr["telefono"]),
                            puesto = Convert.ToInt32(dr["id_tipopuesto"].ToString()),
                            planilla = Convert.ToInt32(dr["id_tipoplanilla"]),
                            estado = Convert.ToInt32(dr["id_estadocolaborador"]),
                        });

                }

                conn.Close();

                return View("Details", colaboradores);
            }
        }

        public ActionResult ReportViewerView1()
        {
            return View();
        }


    }
}
