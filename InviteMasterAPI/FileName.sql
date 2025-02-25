--TABLA EVENTO
CREATE TABLE [dbo].[Evento](
	[IdEvento] [int] IDENTITY(1,1) NOT NULL,
	[NombreEvento] [varchar](100) NOT NULL,
	[FechaEvento] [datetime] NOT NULL,
	[IdCatEventoTipo] [int] NOT NULL,
	[FotoPrincipalUrl] [varchar](max) NULL,
	[FechaCreacion] [nchar](10) NOT NULL,
	[FechaModificacion] [nchar](10) NULL,
	[IdCatEventoStatus] [int] NOT NULL)

GO

--PROCEDURE PATCH EVENTO
ALTER PROCEDURE [dbo].[sp_Evento_Patch]
    @IdEvento INT,
    @NombreEvento VARCHAR(100) = NULL,
    @FechaEvento DATETIME = NULL,
    @FotoPrincipalUrl VARCHAR(MAX) = NULL,
    @CatEventoStatus NCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
	
	DECLARE @IdCatEventoStatus INT = (SELECT IdCatEventoStatus FROM CatEventoStatus WHERE Codigo = @CatEventoStatus AND Activo = 1);

    UPDATE [dbo].[Evento]
    SET 
        [NombreEvento] = COALESCE(@NombreEvento, [NombreEvento]),
        [FechaEvento] = COALESCE(@FechaEvento, [FechaEvento]),
        [FotoPrincipalUrl] = COALESCE(@FotoPrincipalUrl, [FotoPrincipalUrl]),
        [IdCatEventoStatus] = COALESCE(@IdCatEventoStatus, [IdCatEventoStatus]),
        [FechaModificacion] = GETDATE()
    WHERE 
        [IdEvento] = @IdEvento;
END

GO

--TABLA BODA
CREATE TABLE [dbo].[Boda](
	[IdBoda] [int] IDENTITY(1,1) NOT NULL,
	[IdEvento] [int] NOT NULL,
	[Civil] [bit] NOT NULL,
	[CivilLugar] [varchar](100) NULL,
	[CivilDireccion] [varchar](250) NULL,
	[CivilUbicacion] [nvarchar](max) NULL,
	[CivilHorario] [varchar](50) NULL,
	[Iglesia] [bit] NOT NULL,
	[IglesiaLugar] [varchar](100) NULL,
	[IglesiaDireccion] [varchar](250) NULL,
	[IglesiaUbicacion] [nvarchar](max) NULL,
	[IglesiaHorario] [varchar](50) NULL,
	[Local] [bit] NOT NULL,
	[LocalLugar] [varchar](100) NULL,
	[LocalDireccion] [varchar](250) NULL,
	[LocalUbicacion] [nvarchar](max) NULL,
	[LocalHorario] [varchar](50) NULL,
	[NoNinos] [bit] NOT NULL,
	[Padrinos] [varchar](250) NULL,
	[SobreRegalo] [bit] NOT NULL,
	[SobreRegaloTexto] [varchar](50) NULL,
	[Transferencia] [bit] NOT NULL,
	[TransferenciaTexto] [varchar](50) NULL,
	[TransferenciaDatos] [varchar](250) NULL,
	[MesaRegalos] [bit] NOT NULL,
	[MesaRegalosTexto] [varchar](50) NULL,
	[IdCatEtiqueta] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[Activo] [bit] NOT NULL)

GO

--PROCEDURE PATCH BODA
ALTER PROCEDURE [dbo].[sp_Boda_Patch]
    @IdBoda INT,
    @IdEvento INT = NULL,
    @Civil BIT = NULL,
    @CivilLugar VARCHAR(100) = NULL,
    @CivilDireccion VARCHAR(250) = NULL,
    @CivilUbicacion NVARCHAR(MAX) = NULL,
    @CivilHorario VARCHAR(50) = NULL,
    @Iglesia BIT = NULL,
    @IglesiaLugar VARCHAR(100) = NULL,
    @IglesiaDireccion VARCHAR(250) = NULL,
    @IglesiaUbicacion NVARCHAR(MAX) = NULL,
    @IglesiaHorario VARCHAR(50) = NULL,
    @Local BIT = NULL,
    @LocalLugar VARCHAR(100) = NULL,
    @LocalDireccion VARCHAR(250) = NULL,
    @LocalUbicacion NVARCHAR(MAX) = NULL,
    @LocalHorario VARCHAR(50) = NULL,
    @NoNinos BIT = NULL,
    @Padrinos VARCHAR(250) = NULL,
    @SobreRegalo BIT = NULL,
    @SobreRegaloTexto VARCHAR(50) = NULL,
    @Transferencia BIT = NULL,
    @TransferenciaTexto VARCHAR(50) = NULL,
    @TransferenciaDatos VARCHAR(250) = NULL,
    @MesaRegalos BIT = NULL,
    @MesaRegalosTexto VARCHAR(50) = NULL,
    @CatEtiqueta NCHAR(10) = NULL,
    @Activo BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdCatEtiqueta INT = (SELECT IdCatEtiqueta FROM CatEtiqueta WHERE Codigo = @CatEtiqueta AND Activo = 1);

    UPDATE [dbo].[Boda]
    SET 
        [IdEvento] = COALESCE(@IdEvento, [IdEvento]),
        [Civil] = COALESCE(@Civil, [Civil]),
        [CivilLugar] = COALESCE(@CivilLugar, [CivilLugar]),
        [CivilDireccion] = COALESCE(@CivilDireccion, [CivilDireccion]),
        [CivilUbicacion] = COALESCE(@CivilUbicacion, [CivilUbicacion]),
        [CivilHorario] = COALESCE(@CivilHorario, [CivilHorario]),
        [Iglesia] = COALESCE(@Iglesia, [Iglesia]),
        [IglesiaLugar] = COALESCE(@IglesiaLugar, [IglesiaLugar]),
        [IglesiaDireccion] = COALESCE(@IglesiaDireccion, [IglesiaDireccion]),
        [IglesiaUbicacion] = COALESCE(@IglesiaUbicacion, [IglesiaUbicacion]),
        [IglesiaHorario] = COALESCE(@IglesiaHorario, [IglesiaHorario]),
        [Local] = COALESCE(@Local, [Local]),
        [LocalLugar] = COALESCE(@LocalLugar, [LocalLugar]),
        [LocalDireccion] = COALESCE(@LocalDireccion, [LocalDireccion]),
        [LocalUbicacion] = COALESCE(@LocalUbicacion, [LocalUbicacion]),
        [LocalHorario] = COALESCE(@LocalHorario, [LocalHorario]),
        [NoNinos] = COALESCE(@NoNinos, [NoNinos]),
        [Padrinos] = COALESCE(@Padrinos, [Padrinos]),
        [SobreRegalo] = COALESCE(@SobreRegalo, [SobreRegalo]),
        [SobreRegaloTexto] = COALESCE(@SobreRegaloTexto, [SobreRegaloTexto]),
        [Transferencia] = COALESCE(@Transferencia, [Transferencia]),
        [TransferenciaTexto] = COALESCE(@TransferenciaTexto, [TransferenciaTexto]),
        [TransferenciaDatos] = COALESCE(@TransferenciaDatos, [TransferenciaDatos]),
        [MesaRegalos] = COALESCE(@MesaRegalos, [MesaRegalos]),
        [MesaRegalosTexto] = COALESCE(@MesaRegalosTexto, [MesaRegalosTexto]),
        [IdCatEtiqueta] = COALESCE(@IdCatEtiqueta, [IdCatEtiqueta]),
        [FechaModificacion] = GETDATE(),
        [Activo] = COALESCE(@Activo, [Activo])
    WHERE 
        [IdBoda] = @IdBoda;
END

GO
