-------------------------------Grupo 3---------------------------------
-- FUNCTION: public.addflight(integer, double precision, date, date)

-- DROP FUNCTION public.addflight(integer, double precision, date, date);

CREATE OR REPLACE FUNCTION public.addflight(
	_plane integer,
	_price double precision,
	_departure date,
	_arrival date)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$

BEGIN

   INSERT INTO Flight(fli_price ,fli_departureDate, fli_arrivalDate, fli_pla_fk) VALUES
    (_price, _departure, _arrival, _plane);
   RETURN currval('fli_id_seq');
END;

$BODY$;

ALTER FUNCTION public.addflight(integer, double precision, date, date)
    OWNER TO vacanza;

-- FUNCTION: public.addstop(integer, date, date, integer, integer)

-- DROP FUNCTION public.addstop(integer, date, date, integer, integer);

CREATE OR REPLACE FUNCTION public.addstop(
	_flight integer,
	_departure date,
	_arrival date,
	_loc_departure integer,
	_loc_arrival integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$

BEGIN

   INSERT INTO Stop(sto_fli_fk ,sto_departureDate, sto_arrivalDate, sto_locDeparture, sto_locArrival) VALUES
    (_flight, _departure, _arrival, _loc_departure,_loc_arrival);
   RETURN currval('sto_id_seq');
END;

$BODY$;

ALTER FUNCTION public.addstop(integer, date, date, integer, integer)
    OWNER TO vacanza;

-- FUNCTION: public.getplanes()

-- DROP FUNCTION public.getplanes();

CREATE OR REPLACE FUNCTION public.getplanes(
	)
    RETURNS TABLE(id integer, autonomy double precision, isactive boolean, capacity integer, loadingcap double precision, model character varying) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model
	FROM public.Plane;
END;

$BODY$;

ALTER FUNCTION public.getplanes()
    OWNER TO vacanza;

-- FUNCTION: public.findplane(integer)

-- DROP FUNCTION public.findplane(integer);

CREATE OR REPLACE FUNCTION public.findplane(
	_id integer)
    RETURNS TABLE(id integer, autonomy double precision, isactive boolean, capacity integer, loadingcap double precision, model character varying) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model
	FROM public.Plane WHERE _id = pla_id;
END;

$BODY$;

ALTER FUNCTION public.findplane(integer)
    OWNER TO postgres;

-- FUNCTION: public.getroutebyflight(integer)

-- DROP FUNCTION public.getroutebyflight(integer);

CREATE OR REPLACE FUNCTION public.getroutebyflight(
	_id integer)
    RETURNS TABLE(id integer, locdeparture integer, locarrival integer, departuredate date, arrivaldate date) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	sto_id, sto_locdeparture, sto_locarrival, sto_departuredate, sto_arrivaldate
	FROM public.Stop WHERE _id = sto_fli_fk;
END;

$BODY$;

ALTER FUNCTION public.getroutebyflight(integer)
    OWNER TO postgres;


-- FUNCTION: public.getflights()

-- DROP FUNCTION public.getflights();

CREATE OR REPLACE FUNCTION public.getflights()
    RETURNS TABLE(id integer, plane integer, price double precision,
				  departureDate date, arrivalDate date) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	fli_id, fli_pla_fk, fli_price, fli_departuredate, fli_arrivaldate
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
