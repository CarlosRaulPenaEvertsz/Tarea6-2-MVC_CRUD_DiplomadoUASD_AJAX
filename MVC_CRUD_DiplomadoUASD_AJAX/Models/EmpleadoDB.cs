using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_CRUD_DiplomadoUASD_AJAX.Models
{
    public class EmpleadoDB
    {

        // Declaramos el String de Conexion
        string cs = ConfigurationManager.ConnectionStrings["ConnectionStrDB"].ConnectionString;

        // Retornamos una Lista de todos los Empleados
        public List<Empleado> ListAll()
        {
            List<Empleado> lst = new List<Empleado>();
            using (SqlConnection con = new SqlConnection(cs)) 
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmpleado", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while(rdr.Read())
                {
                    lst.Add(new Empleado
                    {
                        EmpleadoID = Convert.ToInt32(rdr["EmpleadoId"]),
                        Nombres = rdr["Nombres"].ToString(),
                        Apellidos = rdr["Apellidos"].ToString(),
                        Edad = Convert.ToInt32(rdr["Edad"]),
                        Estado_Civil = rdr["Estado_Civil"].ToString(),
                        Pais = rdr["Pais"].ToString(),
                    });
                }
                return lst;
            }
        }

        // Metodos para adicionar un Empleado
        public int Add(Empleado emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmpleado",con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.EmpleadoID);
                com.Parameters.AddWithValue("@Nombres", emp.Nombres);
                com.Parameters.AddWithValue("@Apellidos", emp.Apellidos);
                com.Parameters.AddWithValue("@Edad", emp.Edad);
                com.Parameters.AddWithValue("@Estado_Civil", emp.Estado_Civil);
                com.Parameters.AddWithValue("@Pais", emp.Pais);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        // Metodo para Actualizar record del empleado
        public int Update(Empleado emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmpleado", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.EmpleadoID);
                com.Parameters.AddWithValue("@Nombres", emp.Nombres);
                com.Parameters.AddWithValue("@Apellidos", emp.Apellidos);
                com.Parameters.AddWithValue("@Edad", emp.Edad);
                com.Parameters.AddWithValue("@Estado_Civil", emp.Estado_Civil);
                com.Parameters.AddWithValue("@Pais", emp.Pais);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        // Metodo para eliminar un empleado
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteEmpleado", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;

        }
    }
}