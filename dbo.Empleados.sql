﻿CREATE TABLE [dbo].[Empleados] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (MAX) NULL,
    [Habilidades]       NVARCHAR (MAX) NULL,
    [Salario]           MONEY          NOT NULL,
    [FechaContratacion] DATE           DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED ([Id] ASC)
);

