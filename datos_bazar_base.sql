USE [bd_bazar]
GO
SET IDENTITY_INSERT [dbo].[administrador] ON 

INSERT [dbo].[administrador] ([idAdministrador], [cedula], [nombre], [apellido], [telefono], [mail], [usuario], [contra], [activo], [fecha_registro]) VALUES (1, N'0000000000', N'admin', N'admin', N'0000000000', N'mail_example@example.com', N'admin', N'123', 1, GETDATE())
SET IDENTITY_INSERT [dbo].[administrador] OFF
GO
SET IDENTITY_INSERT [dbo].[estado] ON 

INSERT [dbo].[estado] ([idEstado], [estado], [activo]) VALUES (1, N'Pendiente Pago', 1)
INSERT [dbo].[estado] ([idEstado], [estado], [activo]) VALUES (2, N'Cobrado', 1)
INSERT [dbo].[estado] ([idEstado], [estado], [activo]) VALUES (3, N'Enviado', 1)
INSERT [dbo].[estado] ([idEstado], [estado], [activo]) VALUES (4, N'Entregado', 1)
INSERT [dbo].[estado] ([idEstado], [estado], [activo]) VALUES (5, N'Cancelado', 1)
SET IDENTITY_INSERT [dbo].[estado] OFF
GO
