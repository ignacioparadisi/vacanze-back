DROP USER IF EXISTS "vacanza";

CREATE USER "vacanza" WITH
    LOGIN
    SUPERUSER
    INHERIT
    CREATEDB
    CREATEROLE
    ENCRYPTED PASSWORD 'vacanza'
    NOREPLICATION;

CREATE DATABASE "vacanza"
    WITH
    OWNER = "vacanza"
    ENCODING = 'UTF8'
    LC_COLLATE = 'C'
    LC_CTYPE = 'UTF-8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;


CREATE TABLE "Hotel" (
  hot_id SERIAL,
  hot_nombre VARCHAR(100) NOT NULL,
  hot_hora_entrada TIME NOT NULL,
  hot_hora_salida TIME NOT NULL,
  hot_telefono VARCHAR(20) NOT NULL,
  hot_sitio_web VARCHAR(100),
  CONSTRAINT pk_hotel PRIMARY KEY (hot_id)
  -- TODO: Atributos/relaciones por agregar
  --       - Ubicacion
);

CREATE TABLE "Servicio" (
  ser_id SERIAL,
  ser_nombre VARCHAR(100) NOT NULL,
  ser_descripcion VARCHAR(300),
  CONSTRAINT pk_servicio PRIMARY KEY (ser_id)
);

CREATE TABLE "Hotel_Servicio" (
  hot_ser_fk_hot_id INTEGER NOT NULL,
  hot_ser_fk_ser_id INTEGER NOT NULL,
  hot_ser_destacado BOOLEAN NOT NULL DEFAULT FALSE,
  CONSTRAINT fk_hotel_servicio_hotel FOREIGN KEY (hot_ser_fk_hot_id) REFERENCES "Hotel"(hot_id),
  CONSTRAINT fk_hotel_servicio_servicio FOREIGN KEY (hot_ser_fk_ser_id) REFERENCES "Servicio"(ser_id)
);
