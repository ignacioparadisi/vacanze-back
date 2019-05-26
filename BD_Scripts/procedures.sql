-------------------------------Grupo 3---------------------------------
-- FUNCTION: public.addflight(integer, double precision, date, date, integer, integer)

-- DROP FUNCTION public.addflight(integer, double precision, date, date, integer, integer);

CREATE OR REPLACE FUNCTION public.addflight(
	_plane integer,
	_price double precision,
	_departure timestamp without time zone,
	_arrival timestamp without time zone,
	_loc_arrival integer,
	_loc_departure integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$

BEGIN

   INSERT INTO Flight(fli_id,fli_price ,fli_departureDate, fli_arrivalDate, fli_pla_fk, fli_loc_arrival,
					 fli_loc_departure) VALUES
    (nextval('seq_flight'),_price, _departure, _arrival, _plane, _loc_arrival, _loc_departure);
   RETURN currval('seq_flight');
END;

$BODY$;

ALTER FUNCTION public.addflight(integer, double precision, timestamp without time zone, timestamp without time zone, integer, integer)
    OWNER TO vacanza;

-- FUNCTION: public.getplanes()

-- DROP FUNCTION public.getplanes();

CREATE OR REPLACE FUNCTION public.getplanes(
	)
    RETURNS TABLE(id integer, autonomy integer, isactive boolean, capacity integer, loadingcap integer, model character varying) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	pla_id, pla_autonomy, pla_isActive, pla_capacity, pla_loadingCap, pla_model
	FROM public.Plane;
END;

$BODY$;

ALTER FUNCTION public.getplanes()
    OWNER TO vacanza;


-- FUNCTION: public.findplane(integer)

-- DROP FUNCTION public.findplane(integer);

CREATE OR REPLACE FUNCTION public.findplane(
	_id integer)
    RETURNS TABLE(id integer, autonomy integer, isactive boolean, capacity integer, loadingcap integer, model character varying) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	pla_id, pla_autonomy, pla_isActive, pla_capacity, pla_loadingCap, pla_model
	FROM public.Plane WHERE _id = pla_id;
END;

$BODY$;

ALTER FUNCTION public.findplane(integer)
    OWNER TO postgres;

-- FUNCTION: public.getflights()

-- DROP FUNCTION public.getflights();

CREATE OR REPLACE FUNCTION public.getflights(
	)
    RETURNS TABLE(id integer, plane integer, price numeric, departuredate timestamp without time zone, arrivaldate timestamp without time zone, locdeparture integer, locarrival integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	fli_id, fli_pla_fk, fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival
	FROM public.Flight;
END;

$BODY$;

ALTER FUNCTION public.getflights()
    OWNER TO postgres;


------- grupo 6 ----------
CREATE OR REPLACE FUNCTION ConsultarHoteles()
RETURNS TABLE
  (id integer,
   nombre VARCHAR(100),
   cantHuespedes INTEGER,
   status BOOLEAN,
   telefono VARCHAR(20),
   sitioweb VARCHAR(100),
   ciudad VARCHAR(100)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    H.hot_id, H.hot_nombre,H.hot_capHuesped, H.hot_statusActivo,H.hot_telefono,H.hot_sitio_web, L.l_nombre
    FROM Hotel AS H, Lugar AS L WHERE L.l_id = H.fk_lugar;
END;
$$ LANGUAGE plpgsql;


-- grupo 9 -----------------------------------------
-------------AGREGAR Claim-----------------

CREATE OR REPLACE FUNCTION AddClaim(
    _cla_title VARCHAR(20), 
    _cla_description VARCHAR(30)
    ) 
RETURNS integer AS
$$
BEGIN

   INSERT INTO Claim(cla_title, cla_descr, cla_status) VALUES
    ( _cla_title, _cla_description, 'ABIERTO');
   RETURN currval('SEQ_Claim');
END;
$$ LANGUAGE plpgsql;

---------MODIFICAR Reclamo-----------------
CREATE OR REPLACE FUNCTION ModifyClaimStatus( 
    _cla_id integer,
    _cla_status VARCHAR(35))
RETURNS integer AS
$$
BEGIN

   UPDATE Claim SET cla_status= _cla_status
    WHERE (cla_id = _cla_id);
   RETURN _cla_id;
END;
$$ LANGUAGE plpgsql;
-- modificar el titulo del reclam y la descripcion
CREATE OR REPLACE FUNCTION ModifyClaimTitle( 
	_cla_id integer,
    _cla_title VARCHAR(35),
	_cla_descr VARCHAR(30))
RETURNS integer AS
$$
BEGIN

   UPDATE Claim SET cla_title= _cla_title and cla_descr= _cla_descr
	WHERE (cla_id = _cla_id);
   RETURN _cla_id;
END;
$$ LANGUAGE plpgsql;
-------------------------------------ELIMAR Reclamo-----------------------------

CREATE OR REPLACE FUNCTION DeleteClaim(_cla_id integer)
RETURNS void AS
$$
BEGIN

    DELETE FROM Claim 
    WHERE (cla_id = _cla_id);

END;
$$ LANGUAGE plpgsql;
--------------------------CONSULTAR Claim--------------------
CREATE OR REPLACE FUNCTION GetClaim(_cla_id integer)
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cla_id, cla_title,cla_descr, cla_status
    FROM ClaimWHERE cla_id = _cla_id;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR Claim--------------------
CREATE OR REPLACE FUNCTION GetClaimBaggage(_cla_id integer)
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cla_id, cla_title,cla_descr, cla_status
    FROM Claim WHERE cla_id = _cla_id;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR Claim--------------------
CREATE OR REPLACE FUNCTION GetClaimDocumentPasaport(_cla_id integer)
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cla_id, cla_title,cla_descr, cla_status
    FROM Claim WHERE cla_id = _cla_id;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR Claim--------------------
CREATE OR REPLACE FUNCTION GetClaimDocumentCedula(_cla_id integer)
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cla_id, cla_title,cla_descr, cla_status
    FROM Claim WHERE cla_id = _cla_id;
END;
$$ LANGUAGE plpgsql;



------------------------------------fin de grupo 9---------------------------------
