-- ========================================
-- PRUEBA T�CNICA � Script base de datos
-- Autor: Eduardo Luis Zequeira
-- Fecha: 04/07/2025
-- Descripci�n: Se crea la base de datos en conjunto a las diversas tablas que ser�n usadas
-- y se insertan valores en estas. 
-- ========================================

-- ================================================
-- Paso 1: Insertar 5 Proveedores
-- ================================================
INSERT INTO Providers (Id, Nit, Name, Email, CreatedAt, UpdatedAt)
VALUES
  ('11111111-1111-1111-1111-111111111111', '901234561', 'Proveedor Alpha',   'alpha@correo.com',   GETDATE(), GETDATE()),
  ('22222222-2222-2222-2222-222222222222', '901234562', 'Proveedor Beta',    'beta@correo.com',    GETDATE(), GETDATE()),
  ('33333333-3333-3333-3333-333333333333', '901234563', 'Proveedor Gamma',   'gamma@correo.com',   GETDATE(), GETDATE()),
  ('44444444-4444-4444-4444-444444444444', '901234564', 'Proveedor Delta',   'delta@correo.com',   GETDATE(), GETDATE()),
  ('55555555-5555-5555-5555-555555555555', '901234565', 'Proveedor Epsilon', 'epsilon@correo.com', GETDATE(), GETDATE());

-- ================================================
-- Paso 2: Insertar campos personalizados por proveedor
-- ================================================
INSERT INTO CustomFields (Id, ProviderId, [Key], [Value], CreatedAt)
VALUES
  (NEWID(), '11111111-1111-1111-1111-111111111111', 'Ubicaci�n', 'Medell�n', GETDATE()),
  (NEWID(), '11111111-1111-1111-1111-111111111111', 'Antig�edad', '5 a�os', GETDATE()),
  (NEWID(), '22222222-2222-2222-2222-222222222222', 'Certificaci�n', 'ISO 9001', GETDATE()),
  (NEWID(), '33333333-3333-3333-3333-333333333333', 'Especializaci�n', 'Log�stica', GETDATE()),
  (NEWID(), '44444444-4444-4444-4444-444444444444', 'Nivel de riesgo', 'Bajo', GETDATE()),
  (NEWID(), '55555555-5555-5555-5555-555555555555', 'Contrato vigente', 'S�', GETDATE());

-- ================================================
-- Paso 3: Insertar servicios por proveedor
-- ================================================
INSERT INTO Services (Id, ProviderId, Name, HourlyRateUsd, Countries, CreatedAt)
VALUES
  (NEWID(), '11111111-1111-1111-1111-111111111111', 'Consultor�a TI',       120.00, 'Colombia,M�xico',    GETDATE()),
  (NEWID(), '22222222-2222-2222-2222-222222222222', 'Soporte t�cnico',      80.00,  'Per�,Ecuador',       GETDATE()),
  (NEWID(), '33333333-3333-3333-3333-333333333333', 'Despacho log�stico',  100.00, 'Colombia',            GETDATE()),
  (NEWID(), '44444444-4444-4444-4444-444444444444', 'Transporte privado',   75.00,  'Chile,Argentina',     GETDATE()),
  (NEWID(), '55555555-5555-5555-5555-555555555555', 'Instalaci�n t�cnica', 110.00, 'Espa�a',              GETDATE());
