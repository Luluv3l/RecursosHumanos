using Planilla.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Planilla.Controllers
{
    public class usuariosController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";

        // GET: usuarios
        public ActionResult Index()
        {
            return View();
        }

        // GET: usuarios/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        [HttpPost]
        public ActionResult Create(usuarios ousuarios)
        {
            try
            {
                bool registrado;
                string mensaje;

                ousuarios.contrasena = ConvertirSha256(ousuarios.contrasena);
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_nuevo_usuario", conn);
                    cmd.Parameters.AddWithValue("usuario", ousuarios.usuario);
                    cmd.Parameters.AddWithValue("contrasena", ousuarios.contrasena);
                    cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

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

        // GET: usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: usuarios/Edit/5
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

        // GET: usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: usuarios/Delete/5
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

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        [HttpPost]
        public ActionResult Buscar(int id)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {

                string query = ("select id_usuario,usuario from usuarios where id_usuario=@id_usuario ");

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_usuario", id);

                conn.Open();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                sd.Fill(ds);


                List<usuarios> usuarios = new List<usuarios>();
                foreach (DataRow dr in ds.Tables[0].Rows)

                {
                    usuarios.Add(
                        new usuarios
                        {
                            id_usuario = Convert.ToInt32(dr["id_usuario"]),
                            usuario = Convert.ToString(dr["usuario"]),

                        });

                }

                conn.Close();

                return View("Details", usuarios);
            }
        }
    }
}
