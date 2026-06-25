-- Crear base de datos
CREATE DATABASE MantaroInclusivoDB;
GO

USE MantaroInclusivoDB;
GO

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE NOT NULL,
    Telefono NVARCHAR(20),
    ContraseñaHash NVARCHAR(255) NOT NULL,
    PerfilAccesibilidad NVARCHAR(20),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);

-- Tabla de Destinos
CREATE TABLE Destinos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Latitud DECIMAL(10, 7) NOT NULL,
    Longitud DECIMAL(10, 7) NOT NULL,
    Rampas BIT DEFAULT 0,
    BanosAccesibles BIT DEFAULT 0,
    EstacionamientoReservado BIT DEFAULT 0,
    AudioGuias BIT DEFAULT 0,
    RutasTactiles BIT DEFAULT 0,
    Pictogramas BIT DEFAULT 0,
    PersonalCapacitado BIT DEFAULT 0,
    SenalizacionBraille BIT DEFAULT 0,
    ImagenURL NVARCHAR(500),
    Activo BIT DEFAULT 1
);

-- Insertar datos de ejemplo
INSERT INTO Destinos (Nombre, Descripcion, Latitud, Longitud, Rampas, BanosAccesibles, EstacionamientoReservado, AudioGuias, RutasTactiles, Pictogramas, PersonalCapacitado, SenalizacionBraille)
VALUES 
    ('Convento de Ocopa', 'Museo y convento histórico con rampas certificadas.', -12.1246, -75.2345, 1, 1, 1, 1, 0, 0, 0, 0),
    ('Torre Torre', 'Formaciones rocosas únicas, senderos adaptados para silla de ruedas.', -12.0691, -75.2167, 0, 0, 0, 0, 0, 0, 0, 1),
    ('Parque de la Identidad Huanca', 'Recorrido cultural con audioguías y pictogramas.', -12.0699, -75.2105, 0, 0, 0, 1, 1, 1, 1, 0);