using ProyectoEquipo.DataBase;
using ProyectoEquipo.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ProyectoEquipo.Repositories
{
    public class PrestamoRepository
    {
        public List<Prestamo> GetAll()
        {
            var lista = new List<Prestamo>();
            string query = @"SELECT p.Id, p.EquipoId, p.EmpleadoId, p.FechaPrestamo, 
                             p.FechaDevolucion, p.Estado, e.Nombre, em.Nombre
                             FROM Prestamos p
                             JOIN Equipos e ON p.EquipoId = e.Id
                             JOIN Empleados em ON p.EmpleadoId = em.Id";
            using (var reader = DBHelper.ExecuteQuery(query))
            {
                while (reader.Read())
                {
                    lista.Add(new Prestamo
                    {
                        Id = reader.GetInt32(0),
                        EquipoId = reader.GetInt32(1),
                        EmpleadoId = reader.GetInt32(2),
                        FechaPrestamo = reader.GetString(3),
                        FechaDevolucion = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        Estado = reader.GetString(5),
                        NombreEquipo = reader.GetString(6),
                        NombreCliente = reader.GetString(7)
                    });
                }
            }
            return lista;
        }

        public void Insert(Prestamo p)
        {
            string query = @"INSERT INTO Prestamos (EquipoId, EmpleadoId, FechaPrestamo, Estado)
                             VALUES (@eq, @em, @fp, 'Activo');
                             UPDATE Equipos SET Disponible=0 WHERE Id=@eq;";
            var parametros = new[]
            {
                new SQLiteParameter("@eq", p.EquipoId),
                new SQLiteParameter("@em", p.EmpleadoId),
                new SQLiteParameter("@fp", p.FechaPrestamo)
            };
            DBHelper.ExecuteNonQuery(query, parametros);
        }

        public void Devolver(int prestamoId, int equipoId, string fechaDevolucion)
        {
            string query = @"UPDATE Prestamos SET Estado='Devuelto', FechaDevolucion=@fd WHERE Id=@id;
                             UPDATE Equipos SET Disponible=1 WHERE Id=@eqId;";
            var parametros = new[]
            {
                new SQLiteParameter("@fd", fechaDevolucion),
                new SQLiteParameter("@id", prestamoId),
                new SQLiteParameter("@eqId", equipoId)
            };
            DBHelper.ExecuteNonQuery(query, parametros);
        }

        public void Delete(int id)
        {
            DBHelper.ExecuteNonQuery("DELETE FROM Prestamos WHERE Id=@id", new[] { new SQLiteParameter("@id", id) });
        }
    }
}
