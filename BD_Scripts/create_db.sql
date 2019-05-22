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

----------------------------------grupo 9-------------------------------------------

CREATE SEQUENCE SEQ_RECLAMO
  START WITH 1
  INCREMENT BY 1
  NO MINVALUE
  NO MAXVALUE
  CACHE 1;
  
CREATE TABLE RECLAMO(
    rec_id integer,
    rec_titulo varchar(30) NOT NULL,
    rec_descr varchar(30) NOT NULL,
    rec_status varchar(20) CHECK (rec_status ='ABIERTO' OR rec_status='CERRADO' OR rec_status='ESPERA'), 
    CONSTRAINT pk_reclamo PRIMARY KEY(rec_id)
    --CONSTRAINT pf_equipaje FOREIGN KEY (rec_equi_id) REFERENCES JUGADOR(equi_id) ON DELETE CASCADE ON UPDATE CASCADE, 
);

-------------AGREGAR RECLAMO-----------------

CREATE OR REPLACE FUNCTION AgregarReclamo(
	_titulo VARCHAR(20), 
	_descripcion VARCHAR(30),
 	_status VARCHAR(30)
 	) 
RETURNS integer AS
$$
BEGIN

   INSERT INTO RECLAMO(rec_id ,rec_titulo, rec_descr, rec_status) VALUES
    (nextval('SEQ_RECLAMO'), _titulo, _descripcion, _status);
   RETURN currval('SEQ_RECLAMO');
END;
$$ LANGUAGE plpgsql;

---------MODIFICAR RECAMO-----------------
CREATE OR REPLACE FUNCTION ModificarReclamoStatus( 
	_idReclamo integer,
    _status VARCHAR(35))
RETURNS integer AS
$$
BEGIN

   UPDATE RECLAMO SET rec_status= _status
	WHERE (rec_id = _idReclamo);
   RETURN _idReclamo;
END;
$$ LANGUAGE plpgsql;
-------------------------------------ELIMAR RECLAMO-----------------------------
CREATE OR REPLACE FUNCTION EliminarReclamo(_idReclamo integer)
RETURNS void AS
$$
BEGIN

    DELETE FROM RECLAMO 
    WHERE (rec_id= _idReclamo);

END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR LOGROS CANTIDAD PENDIENTE--------------------
CREATE OR REPLACE FUNCTION ConsultarUnReclamo(_idReclamo integer)
RETURNS TABLE
  (id integer,
   Titulo VARCHAR(30),
   Descripcion VARCHAR(30),
   Status VARCHAR(30)
  )
AS
$$
BEGIN
	RETURN QUERY SELECT
	rec_id, rec_titulo,rec_descr, rec_status
	FROM RECLAMO WHERE rec_id = _idReclamo;
END;
$$ LANGUAGE plpgsql;

------------------------------------fin de grupo 9---------------------------------