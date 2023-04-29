using Planilla.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Planilla.Controllers
{
    public class sistemaController : Controller
    {
        static string conexion = "Data Source=localhost;Initial Catalog = PROYECTO; Integrated Security = True";
        // GET: sistema
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(usuarios usuarios)
        {
            usuarios.contrasena = ConvertirSha256(usuarios.contrasena);
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_validar_usuario", conn);
                cmd.Parameters.AddWithValue("usuario", usuarios.usuario);
                cmd.Parameters.AddWithValue("contrasena", usuarios.contrasena);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                usuarios.id_usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (usuarios.id_usuario != 0)
            {
                Session["usuario"] = usuarios;
                return View("menubar");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no wncontrado";
                return View();
            }
        }

        public static string ConvertirSha256(string texto)
        {

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


        public ActionResult Sistema()
        {
            return View();
        }

        public ActionResult cerrarsesion()
        {

            Session["usuario"] = null;
            return RedirectToAction("Index", "sistema");
        }

    }
}
