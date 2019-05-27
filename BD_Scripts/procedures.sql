-------------------------------Grupo 3---------------------------------
-- FUNCTION: public.addflight(integer, double precision, timestamp without time zone, timestamp without time zone, integer, integer)

-- DROP FUNCTION public.addflight(integer, double precision, timestamp without time zone, timestamp without time zone, integer, integer);

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

-- FUNCTION: public.getflightsbydate(timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION public.getflightsbydate(timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.getflightsbydate(
	_begin timestamp without time zone,
	_end timestamp without time zone)
    RETURNS TABLE(id integer, plane integer, price numeric, departuredate timestamp without time zone, arrivaldate timestamp without time zone, locdeparture integer, locarrival integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	fli_id, fli_pla_fk, fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival
	FROM public.Flight WHERE fli_departuredate BETWEEN _begin AND _end + '1 days'::interval;
END;

$BODY$;

ALTER FUNCTION public.getflightsbydate(timestamp without time zone, timestamp without time zone)
    OWNER TO postgres;


------- grupo 2 ----------
CREATE OR REPLACE FUNCTION GetRoles()
RETURNS TABLE
  (id integer,
   nombre VARCHAR(50)
  )
AS
$$
BEGIN
  RETURN QUERY SELECT *
  FROM Role;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetRolesForUser(user_id bigint)
    RETURNS TABLE
            (id integer,
             nombre VARCHAR(50)
            )
AS
$$
BEGIN
    RETURN QUERY SELECT role.* FROM Role AS role, User_Role 
    WHERE usr_rol_id = role.rol_id AND usr_use_id = user_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetEmployees()
RETURNS TABLE
    (id integer, 
    documentId VARCHAR(50),
    name VARCHAR(50), 
    lastname VARCHAR(50), 
    email VARCHAR(50))
AS
$$
    BEGIN
        RETURN QUERY SELECT DISTINCT use_id, use_document_id, use_name, use_last_name, use_email 
        FROM Users, User_Role WHERE use_id = usr_use_id AND usr_rol_id <> 1;
    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetUserByEmail(email_id VARCHAR(30))
  RETURNS TABLE
          (id integer,
           documentId VARCHAR(50),
           name VARCHAR(50),
           lastname VARCHAR(50),
           email VARCHAR(50))
AS
$$
BEGIN
  RETURN QUERY SELECT use_id, use_document_id, use_name, use_last_name, use_email
               FROM Users WHERE use_email = email_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION AddUser(doc_id VARCHAR(20),
                                        name VARCHAR(30),
                                        lastname VARCHAR(30),
                                        email VARCHAR(30),
                                        password VARCHAR(50))
  RETURNS VOID AS
$$
BEGIN
  INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
  VALUES (doc_id, name, lastname, email, password);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION AddUser_Role(rol_id INTEGER,
                                             use_id INTEGER)
  RETURNS VOID AS
$$
BEGIN
  INSERT INTO User_Role(usr_rol_id, usr_use_id)
  VALUES (rol_id, use_id);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION DeleteUserByEmail(email_id VARCHAR(30))
  RETURNS VOID AS
$$
BEGIN
  DELETE FROM Users WHERE use_email = email_id;
END;
$$ LANGUAGE plpgsql;

------- grupo 6 ----------
CREATE OR REPLACE FUNCTION AddHotel(name VARCHAR(100),
                                    amountOfRooms INTEGER,
                                    active BOOLEAN,
                                    phone VARCHAR(30),
                                    website VARCHAR(30),
                                    location INTEGER)
    RETURNS integer AS
$$
DECLARE
    dest_id INTEGER;
BEGIN
    INSERT INTO hotel (hot_nombre, hot_cant_habitaciones, hot_activo, hot_telefono, hot_sitio_web,
                       hot_fk_lugar)
    VALUES (name, amountOfRooms, active, phone, website, location) RETURNING hot_id INTO dest_id;
    RETURN dest_id;
END;
$$ LANGUAGE plpgsql;

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
    H.hot_id, H.hot_nombre, H.hot_cant_habitaciones , H.hot_activo ,H.hot_telefono,H.hot_sitio_web, L.l_nombre
    FROM Hotel AS H, Lugar AS L WHERE L.l_id = H.hot_fk_lugar;
END;
$$ LANGUAGE plpgsql;

------------------------------------ grupo 8 --------------------------------------

---------Agregar Ship-------------------
CREATE OR REPLACE FUNCTION AddShip( 
  _shi_name VARCHAR(20),
  _shi_capacity INTEGER,
  _shi_loadingcap INTEGER,
  _shi_model VARCHAR(20),
  _shi_line VARCHAR(30),
  _shi_picture VARCHAR
  ) 
RETURNS integer AS
$$
BEGIN

   INSERT INTO Ship(shi_id, shi_name, shi_capacity ,shi_loadingcap, shi_model,
                    shi_line, shi_picture ) VALUES
    (nextval('seq_ship'), _shi_name, _shi_capacity, _shi_loadingcap, _shi_model, _shi_line, _shi_picture );
   RETURN currval('SEQ_SHIP');
END;
$$ LANGUAGE plpgsql;

-------Agregar Crucero--------------------

CREATE OR REPLACE FUNCTION AddCruise( 
  _cru_shi_fk INTEGER,
  _cru_departuredate TIMESTAMP,
  _cru_arrivaldate TIMESTAMP,
  _cru_price DECIMAL,
  _cru_loc_arrival INTEGER,
  _cru_loc_departure INTEGER
  ) 
RETURNS integer AS
$$
BEGIN

   INSERT INTO Cruise(cru_id, cru_shi_fk, cru_departuredate, cru_arrivaldate, cru_price,
                       cru_loc_arrival, cru_loc_departure ) VALUES
    (nextval('seq_cruise'), _cru_shi_fk, _cru_departuredate, _cru_arrivaldate, _cru_price, _cru_loc_arrival, _cru_loc_departure );
   RETURN currval('SEQ_CRUISE');
END;
$$ LANGUAGE plpgsql;

--------Eliminar Ship--------------------
CREATE OR REPLACE FUNCTION DeleteShip(_shi_id integer)
RETURNS void AS
$$
BEGIN

    DELETE FROM Ship 
    WHERE (shi_id = _shi_id);

END;
$$ LANGUAGE plpgsql;
--------Eliminar Cruise------------------
CREATE OR REPLACE FUNCTION DeleteCruise(_cru_id integer)
RETURNS void AS
$$
BEGIN

    DELETE FROM Cruise 
    WHERE (cru_id = _cru_id);

END;
$$ LANGUAGE plpgsql;
--------Modificar Ship-------------------
CREATE OR REPLACE FUNCTION ModifyShip( 
    _shi_id integer,
    _shi_isactive boolean,
    _shi_name VARCHAR(20),
    _shi_capacity integer,
    _shi_loadingcap integer,
    _shi_model varchar(20),
    _shi_line varchar(30),
    _shi_picture varchar)
RETURNS integer AS
$$
BEGIN

   UPDATE Ship SET shi_isactive = _shi_isactive,
    shi_name = _shi_name, shi_capacity = _shi_capacity,
    shi_loadingcap = _shi_loadingcap, shi_model = _shi_model,
    shi_line = _shi_line, shi_picture = _shi_picture
    WHERE (shi_id = _shi_id);
   RETURN _shi_id;
END;
$$ LANGUAGE plpgsql;


--------Modificar Cruise-----------------

CREATE OR REPLACE FUNCTION ModifyCruiseDepartureDate( 
    _cru_id integer,
    _cru_departuredate TIMESTAMP,
    _cru_arivaldate TIMESTAMP,
    _cru_price DECIMAL
    )
RETURNS integer AS
$$
BEGIN

   UPDATE Cruise SET cru_departuredate= _cru_departuredate,
   cru_arrivaldate = _cru_arivaldate, cru_price = _cru_price
    WHERE (cru_id = _cru_id);
   RETURN _cru_id;
END;
$$ LANGUAGE plpgsql;


--------Consultar Ship-------------------
CREATE OR REPLACE FUNCTION GetShip(_shi_id integer)
RETURNS TABLE
  (id integer,
   name VARCHAR(30),
   status VARCHAR(30),
   capacity_people VARCHAR(30),
   capacity_tonnes VARCHAR(30),
   model VARCHAR(30),
   cruise_line VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    shi_id, shi_name, shi_isactive, shi_capacity, shi_loadingcap, shi_model, shi_line
    FROM Ship WHERE shi_id = _shi_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetShipPic(_shi_id integer)
RETURNS varchar language sql
AS
$$
    SELECT
    shi_picture
    FROM Ship WHERE shi_id = _shi_id;
$$;
--------Consultar Cruise-----------------

CREATE OR REPLACE FUNCTION GetCruise(_cru_id integer)
RETURNS TABLE
  (id integer,
   ship VARCHAR(30),
   departure_date VARCHAR(30),
   arrival_date VARCHAR(30),
   price VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cru_id, cru_shi_fk, cru_departuredate, cru_arrivaldate, cru_price
    FROM Cruise WHERE cru_id = _cru_id;
END;
$$ LANGUAGE plpgsql;
------------------------------------fin de grupo 8---------------------------------

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
    FROM ClaimWHERE WHERE cla_id = _cla_id;
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
------ Consulta de los lugares ------
CREATE OR REPLACE FUNCTION GetLocations()
RETURNS TABLE
  (id integer,
   city VARCHAR(30),
   country VARCHAR(30))
AS
$$
BEGIN
    RETURN QUERY SELECT
    LOC_ID, LOC_CITY, LOC_COUNTRY
    FROM LOCATION;
END;
$$ LANGUAGE plpgsql;