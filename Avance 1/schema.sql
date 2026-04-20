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
    Estado TEXT NOT NULL DEFAULT 'Activo',
    FOREIGN KEY (EquipoId) REFERENCES Equipos(Id),
    FOREIGN KEY (EmpleadoId) REFERENCES Empleados(Id)
);

-- Datos de prueba
INSERT INTO Equipos (Nombre, Descripcion, Disponible) VALUES ('Laptop Dell', 'Core i7 16GB RAM', 1);
INSERT INTO Equipos (Nombre, Descripcion, Disponible) VALUES ('Cámara Canon', 'EOS Rebel T7', 1);
INSERT INTO Equipos (Nombre, Descripcion, Disponible) VALUES ('Proyector Epson', 'HDMI 1080p', 1);

INSERT INTO Empleados (Nombre, Departamento, Email) VALUES ('Ana García', 'TI', 'ana@empresa.com');
INSERT INTO Empleados (Nombre, Departamento, Email) VALUES ('Luis Pérez', 'Marketing', 'luis@empresa.com');