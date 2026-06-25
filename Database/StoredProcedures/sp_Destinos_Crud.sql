USE MantaroInclusivoDB;
GO

CREATE OR ALTER PROCEDURE sp_Destinos_Crud
    @Operacion NVARCHAR(20),
    @Id INT = NULL,
    @PerfilAccesibilidad NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operacion = 'SELECT'
    BEGIN
        SELECT Id, Nombre, Descripcion, Latitud, Longitud,
               Rampas, BanosAccesibles, EstacionamientoReservado,
               AudioGuias, RutasTactiles, Pictogramas,
               PersonalCapacitado, SenalizacionBraille,
               ImagenURL, Activo
        FROM Destinos
        WHERE Activo = 1
        ORDER BY Nombre;
    END
    ELSE IF @Operacion = 'SELECTBYID'
    BEGIN
        SELECT Id, Nombre, Descripcion, Latitud, Longitud,
               Rampas, BanosAccesibles, EstacionamientoReservado,
               AudioGuias, RutasTactiles, Pictogramas,
               PersonalCapacitado, SenalizacionBraille,
               ImagenURL, Activo
        FROM Destinos
        WHERE Id = @Id AND Activo = 1;
    END
    ELSE IF @Operacion = 'SELECTBYPERFIL'
    BEGIN
        SELECT Id, Nombre, Descripcion, Latitud, Longitud,
               Rampas, BanosAccesibles, EstacionamientoReservado,
               AudioGuias, RutasTactiles, Pictogramas,
               PersonalCapacitado, SenalizacionBraille,
               ImagenURL, Activo,
               CASE @PerfilAccesibilidad
                   WHEN 'motriz' THEN CAST(Rampas AS INT) + CAST(BanosAccesibles AS INT) + CAST(EstacionamientoReservado AS INT)
                   WHEN 'visual' THEN CAST(RutasTactiles AS INT) + CAST(SenalizacionBraille AS INT) + CAST(AudioGuias AS INT)
                   WHEN 'auditiva' THEN CAST(Pictogramas AS INT) + CAST(PersonalCapacitado AS INT)
                   WHEN 'cognitiva' THEN CAST(Pictogramas AS INT) + CAST(PersonalCapacitado AS INT) + CAST(AudioGuias AS INT)
                   ELSE 0
               END AS NivelAccesibilidad
        FROM Destinos
        WHERE Activo = 1
        ORDER BY NivelAccesibilidad DESC;
    END
END;
GO