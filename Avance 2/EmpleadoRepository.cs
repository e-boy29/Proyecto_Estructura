using ProyectoEquipo.DataBase;
using ProyectoEquipo.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ProyectoEquipo.Repositories
{
    public class ClienteRepository
    {
        public List<Cliente> GetAll()
        {
            var lista = new List<Cliente>();
            using (var reader = DBHelper.ExecuteQuery("SELECT * FROM Empleados"))
            {
                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Departamento = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Email = reader.IsDBNull(3) ? "" : reader.GetString(3)
                    });
                }
            }
            return lista;
        }

        public void Insert(Cliente c)
        {
            string query = "INSERT INTO Empleados (Nombre, Departamento, Email) VALUES (@n, @d, @em)";
            var parametros = new[]
            {
                new SQLiteParameter("@n", c.Nombre),
                new SQLiteParameter("@d", c.Departamento),
                new SQLiteParameter("@em", c.Email)
            };
            DBHelper.ExecuteNonQuery(query, parametros);
        }

        public void Update(Cliente c)
        {
            string query = "UPDATE Empleados SET Nombre=@n, Departamento=@d, Email=@em WHERE Id=@id";
            var parametros = new[]
            {
                new SQLiteParameter("@n", c.Nombre),
                new SQLiteParameter("@d", c.Departamento),
                new SQLiteParameter("@em", c.Email),
                new SQLiteParameter("@id", c.Id)
            };
            DBHelper.ExecuteNonQuery(query, parametros);
        }

        public void Delete(int id)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM Empleados WHERE Id=@id", new[] { new SQLiteParameter("@id", id) });
        }
    }
}