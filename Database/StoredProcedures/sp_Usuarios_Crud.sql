USE MantaroInclusivoDB;
GO

CREATE OR ALTER PROCEDURE sp_Usuarios_Crud
    @Operacion NVARCHAR(20),
    @Id INT = NULL,
    @Nombres NVARCHAR(100) = NULL,
    @Apellidos NVARCHAR(100) = NULL,
    @Email NVARCHAR(150) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @ContraseñaHash NVARCHAR(255) = NULL,
    @PerfilAccesibilidad NVARCHAR(20) = NULL,
    @Activo BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operacion = 'INSERT'
    BEGIN
        INSERT INTO Usuarios (Nombres, Apellidos, Email, Telefono, ContraseñaHash, PerfilAccesibilidad)
        VALUES (@Nombres, @Apellidos, @Email, @Telefono, @ContraseñaHash, @PerfilAccesibilidad);
        SELECT SCOPE_IDENTITY() AS Id;
    END
    ELSE IF @Operacion = 'SELECT'
    BEGIN
        SELECT Id, Nombres, Apellidos, Email, Telefono, PerfilAccesibilidad, FechaRegistro, Activo
        FROM Usuarios
        WHERE (@Activo IS NULL OR Activo = @Activo)
        ORDER BY FechaRegistro DESC;
    END
    ELSE IF @Operacion = 'SELECTBYID'
    BEGIN
        SELECT Id, Nombres, Apellidos, Email, Telefono, PerfilAccesibilidad, FechaRegistro, Activo
        FROM Usuarios
        WHERE Id = @Id;
    END
    ELSE IF @Operacion = 'SELECTBYEMAIL'
    BEGIN
        SELECT Id, Nombres, Apellidos, Email, Telefono, ContraseñaHash, PerfilAccesibilidad, FechaRegistro, Activo
        FROM Usuarios
        WHERE Email = @Email;
    END
    ELSE IF @Operacion = 'UPDATE'
    BEGIN
        UPDATE Usuarios
        SET 
            Nombres = ISNULL(@Nombres, Nombres),
            Apellidos = ISNULL(@Apellidos, Apellidos),
            Email = ISNULL(@Email, Email),
            Telefono = ISNULL(@Telefono, Telefono),
            ContraseñaHash = ISNULL(@ContraseñaHash, ContraseñaHash),
            PerfilAccesibilidad = ISNULL(@PerfilAccesibilidad, PerfilAccesibilidad),
            Activo = ISNULL(@Activo, Activo)
        WHERE Id = @Id;
        SELECT @Id AS Id;
    END
    ELSE IF @Operacion = 'DELETE'
    BEGIN
        UPDATE Usuarios SET Activo = 0 WHERE Id = @Id;
        SELECT @Id AS Id;
    END
END;
GO