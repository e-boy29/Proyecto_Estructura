using System;
using System.Data.SQLite;
using System.IO;

namespace ProyectoEquipo.DataBase
{
    public class DBHelper
    {
        private static readonly string dbPath = "database/prestamos.db";
        private static readonly string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InicializarBD()
        {
            if (!Directory.Exists("database"))
                Directory.CreateDirectory("database");

            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            string schema = @"
                CREATE TABLE IF NOT EXISTS Equipos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Descripcion TEXT,
                    Disponible INTEGER NOT NULL DEFAULT 1
                );
                CREATE TABLE IF NOT EXISTS Empleados (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Departamento TEXT,
                    Email TEXT
                );
                CREATE TABLE IF NOT EXISTS Prestamos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EquipoId INTEGER NOT NULL,
                    EmpleadoId INTEGER NOT NULL,
                    FechaPrestamo TEXT NOT NULL,
                    FechaDevolucion TEXT,
                    Estado TEXT NOT NULL DEFAULT 'Activo'
                );";

            ExecuteNonQuery(schema);
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void ExecuteNonQuery(string query, SQLiteParameter[] parametros = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    if (parametros != null)
                        cmd.Parameters.AddRange(parametros);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SQLiteDataReader ExecuteQuery(string query, SQLiteParameter[] parametros = null)
        {
            var conn = GetConnection();
            conn.Open();
            var cmd = new SQLiteCommand(query, conn);
            if (parametros != null)
                cmd.Parameters.AddRange(parametros);
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
    }
}
