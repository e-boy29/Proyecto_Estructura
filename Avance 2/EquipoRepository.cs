using ProyectoEquipo.DataBase;
using ProyectoEquipo.Models;
using System.Collections.Generic;
using System.Data.SQLite;   

namespace ProyectoEquipo.Repositories
{
    public class EquipoRepository
    {
        public List<Equipo> GetAll()
        {
            var lista = new List<Equipo>();
            using (var reader = DBHelper.ExecuteQuery("SELECT * FROM Equipos"))
            {
                while (reader.Read())
                {
                    lista.Add(new Equipo
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Disponible = reader.GetInt32(3) == 1
                    });
                }
            }
            return lista;
        }

        public List<Equipo> GetDisponibles()
        {
            var lista = new List<Equipo>();
            using (var reader = DBHelper.ExecuteQuery("SELECT * FROM Equipos WHERE Disponible=1"))
            {
                while (reader.Read())
                {
                    lista.Add(new Equipo
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Disponible = true
                    });
                }
            }
            return lista;
        }

        public void Insert(Equipo e)
        {
            string query = "INSERT INTO Equipos (Nombre, Descripcion, Disponible) VALUES (@n, @d, @disp)";
            var parametros = new[]
            {
                new SQLiteParameter("@n", e.Nombre),
                new SQLiteParameter("@d", e.Descripcion),
                new SQLiteParameter("@disp", e.Disponible ? 1 : 0)
            };
            DBHelper.ExecuteNonQuery(query, parametros);
        }

        public void Update(Equipo e)
        {
            string query = "UPDATE Equipos SET Nombre=@n, Descripcion=@d, Disponible=@disp WHERE Id=@id";
            var parametros = new[]
            {
                new SQLiteParameter("@n", e.Nombre),
                new SQLiteParameter("@d", e.Descripcion),
                new SQLiteParameter("@disp", e.Disponible ? 1 : 0),
                new SQLiteParameter("@id", e.Id)
            };
            DBHelper.ExecuteNonQuery(query, parametros);
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Equipos WHERE Id=@id";
            DBHelper.ExecuteNonQuery(query, new[] { new SQLiteParameter("@id", id) });
        }
    }
}