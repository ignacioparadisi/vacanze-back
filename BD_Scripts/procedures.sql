-------------------------------Grupo 3---------------------------------
-- FUNCTION: public.addflight(integer, double precision, character varying, character varying, integer, integer)

-- DROP FUNCTION public.addflight(integer, double precision,character varying , character varying, integer, integer);

CREATE OR REPLACE FUNCTION public.addflight(
	_plane integer,
	_price double precision,
	_departure character varying,
	_arrival character varying,
  _loc_departure integer,
	_loc_arrival integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    
AS $BODY$
DECLARE
  _fli_id INTEGER;
BEGIN

   INSERT INTO Flight(fli_price ,fli_departureDate, fli_arrivalDate, fli_pla_fk,fli_loc_departure, fli_loc_arrival)
    VALUES
    (_price, TO_TIMESTAMP(_departure,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone, TO_TIMESTAMP(_arrival,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone, _plane, _loc_departure, _loc_arrival)
	RETURNING fli_id into _fli_id;
   RETURN _fli_id;
END;

$BODY$;

ALTER FUNCTION public.addflight(integer, double precision, character varying, character varying, integer, integer)
    OWNER TO vacanza;



-- FUNCTION: public.updateflight(integer, integer, double precision, date, date, integer, integer)

-- DROP FUNCTION public.updateflight(integer, integer, double precision, date, date, integer, integer);
CREATE OR REPLACE FUNCTION public.updateflight( 
  _id integer,
  _plane integer,
	_price double precision,
	_departure character varying,
	_arrival character varying,
	_loc_departure integer,
  _loc_arrival integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$

BEGIN

    UPDATE Flight SET 
    fli_pla_fk = _plane,
    fli_price = _price,
    fli_departuredate = TO_TIMESTAMP(_departure,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone,
    fli_arrivaldate = TO_TIMESTAMP(_arrival,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone,
    fli_loc_departure = _loc_departure,
    fli_loc_arrival = _loc_arrival
    WHERE (fli_id = _id);
   RETURN _id;
END;
$BODY$; 

ALTER FUNCTION public.updateflight(integer, integer, double precision, character varying, character varying, integer, integer)
    OWNER TO vacanza;


-- FUNCTION: public.deleteflight()

-- DROP FUNCTION public.deleteflight();
CREATE OR REPLACE FUNCTION public.deleteflight
(_id integer)
RETURNS integer
LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$
BEGIN

    DELETE FROM Flight 
    WHERE (fli_id = _id);
    return _id;
END;
$BODY$;

ALTER FUNCTION public.deleteflight(integer)
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
    OWNER TO vacanza;


-- FUNCTION: public.findflight(integer)

-- DROP FUNCTION public.findflight(integer);

CREATE OR REPLACE FUNCTION public.findflight(
	_id integer)
    RETURNS TABLE(id integer, price numeric, departuredate timestamp, arrivaldate timestamp, loc_departure integer, loc_arrival integer, pla_fk integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	fli_id, fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival, fli_pla_fk
	FROM public.Flight WHERE _id = fli_id;
END; 

$BODY$;

ALTER FUNCTION public.findflight(integer)
    OWNER TO vacanza;


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
    OWNER TO vacanza;

-- FUNCTION: public.getflightsbydate(timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION public.getflightsbydate(timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.getflightsbydate(
	_begin char varying,
	_end char varying)
    RETURNS TABLE(id integer, plane integer, price numeric, departuredate timestamp without time zone, arrivaldate timestamp without time zone, locdeparture integer, locarrival integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
	fli_id, fli_pla_fk, fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival
	FROM public.Flight WHERE fli_departuredate BETWEEN _begin::timestamp without time zone AND _end::timestamp without time zone + '1 days'::interval;
END;

$BODY$;

ALTER FUNCTION public.getflightsbydate(char varying, char varying)
    OWNER TO vacanza;

--------------------------------------------------------------------------------------------------------------------------------------------------------------
-- FUNCTION: public.getOutBoundFlights(integer, integer, timestamp without time zone)

-- DROP FUNCTION public.getOutBoundFlights(integer, integer, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.getOutBoundFlights(  
	_departure integer,
	_arrival integer,
  _departuredate char varying)
    RETURNS TABLE(id integer, plane integer, price numeric, departuredate timestamp without time zone, arrivaldate timestamp without time zone, locdeparture integer, locarrival integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
  fli_id, fli_pla_fk, fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival
	FROM public.Flight WHERE fli_departuredate::date = _departuredate::timestamp without time zone  and fli_loc_departure = _departure and fli_loc_arrival = _arrival;
END;

$BODY$;

ALTER FUNCTION public.getOutBoundFlights(integer, integer, char varying)
    OWNER TO vacanza; 


-- FUNCTION: public.getRounTripFlights(integer, integer, timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION public.getRounTripFlights(integer, integer, timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public.getRounTripFlights(  
	_departure integer,
	_arrival integer,
  _departuredate char varying,
  _arrivaldate char varying)
    RETURNS TABLE(id integer, plane integer, price numeric, departuredate timestamp without time zone, arrivaldate timestamp without time zone, locdeparture integer, locarrival integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
  fli_id, fli_pla_fk, fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival
	FROM public.Flight WHERE fli_departuredate::date = _departuredate::timestamp without time zone and fli_arrivaldate::date = _arrivaldate::timestamp without time zone 
  and fli_loc_departure = _departure and fli_loc_arrival = _arrival;
END;

$BODY$;

ALTER FUNCTION public.getRounTripFlights(integer, integer, char varying, char varying)
    OWNER TO vacanza; 


-- FUNCTION: public.getflightsbylocation(integer, integer)

-- DROP FUNCTION public.getflightsbylocation(integer, integer);

CREATE OR REPLACE FUNCTION public.getflightsbylocation(
  _departure integer,
	_arrival integer
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
	FROM public.Flight WHERE fli_loc_arrival = _arrival AND fli_loc_departure = _departure;
END;

$BODY$;

ALTER FUNCTION public.getflightsbylocation(integer, integer)
    OWNER TO vacanza;


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

CREATE OR REPLACE FUNCTION GetRolesForUser(user_id INTEGER)
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

CREATE OR REPLACE FUNCTION GetUserById(user_id INTEGER)
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
               FROM Users WHERE use_id = user_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION AddUser(doc_id VARCHAR(20),
                                        name VARCHAR(30),
                                        lastname VARCHAR(30),
                                        email VARCHAR(30),
                                        password VARCHAR(50))
  RETURNS INTEGER AS
$$
DECLARE
  id INTEGER;
BEGIN
  INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
  VALUES (doc_id, name, lastname, email, password) RETURNING use_id INTO id;
  RETURN id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION AddUser_Role(use_id INTEGER, rol_id INTEGER)
  RETURNS INTEGER AS
$$
DECLARE
  id INTEGER;
BEGIN
  INSERT INTO User_Role(usr_rol_id, usr_use_id)
  VALUES (rol_id, use_id) RETURNING usr_id INTO id;
  RETURN id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION DeleteUserByEmail(email_id VARCHAR(30))
  RETURNS INTEGER AS
$$
DECLARE
  id INTEGER;
BEGIN
  DELETE FROM Users WHERE use_email = email_id RETURNING use_id INTO id;
  RETURN id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION DeleteUserById(user_id INTEGER)
RETURNS INTEGER AS 
    $$ 
    DECLARE id INTEGER;
        BEGIN
        DELETE FROM Users WHERE use_id = user_id RETURNING user_id INTO id;
        RETURN id;
        END;
    $$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION DeleteUser_Role(user_id INTEGER)
  RETURNS TABLE (use_id INTEGER) AS
$$
BEGIN
  RETURN QUERY DELETE FROM User_Role WHERE usr_use_id = user_id RETURNING usr_use_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION ModifyUser(id INTEGER,
                                      doc_id VARCHAR(20),
                                      name VARCHAR(30),
                                      lastname VARCHAR(30),
                                      email VARCHAR(30))
  RETURNS integer AS
$$
DECLARE
  user_id integer;
BEGIN
  UPDATE Users SET Use_name = name, Use_last_name = lastname, Use_document_id = doc_id, Use_email = email
  WHERE use_id = id
        RETURNING use_id INTO user_id;
  RETURN user_id;
END;
$$ LANGUAGE plpgsql;

------- grupo 6 ----------
CREATE OR REPLACE FUNCTION AddHotel(name VARCHAR(100),
                                    amountOfRooms INTEGER,
                                    capacityPerRoom INTEGER,
                                    active BOOLEAN,
                                    addressSpecs VARCHAR(200),
                                    roomPrice DECIMAL,
                                    website VARCHAR,
                                    phone VARCHAR,
                                    picture VARCHAR,
                                    stars INTEGER,
                                    location INTEGER)
    RETURNS integer AS
$$
DECLARE
    dest_id INTEGER;
BEGIN
    INSERT INTO HOTEL (hot_name, hot_room_qty, hot_room_capacity, hot_address_specs, hot_room_price,
                       hot_website, hot_phone, hot_picture, hot_stars, hot_loc_fk, hot_is_active)
    VALUES (name, amountOfRooms, capacityPerRoom, addressSpecs, roomPrice, website, phone, picture,
            stars, location, active) RETURNING HOT_ID INTO dest_id;
    RETURN dest_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetHotels()
RETURNS TABLE
  (id integer,
   name VARCHAR(100),
   roomQuantity INTEGER,
   roomCapacity INTEGER,
   isActive BOOLEAN,
   addressSpecs VARCHAR(200),
   pricePerRoom DECIMAL,
   website VARCHAR(100),
   phone VARCHAR(20),
   picture VARCHAR,
   stars INTEGER,
   location INTEGER
  )
AS
$$
BEGIN
    RETURN QUERY SELECT H.HOT_ID,
                        H.HOT_NAME,
                        H.hot_room_qty,
                        H.hot_room_capacity,
                        H.hot_is_active,
                        H.hot_address_specs,
                        H.hot_room_price,
                        H.hot_website,
                        H.hot_phone,
                        H.HOT_PICTURE,
                        H.HOT_STARS,
                        H.hot_loc_fk
                 FROM Hotel AS H;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetHotelById(p_id INTEGER)
    RETURNS TABLE
            (
                id integer,
                name VARCHAR(100),
                roomQuantity INTEGER,
                roomCapacity INTEGER,
                isActive BOOLEAN,
                addressSpecs VARCHAR(200),
                pricePerRoom DECIMAL,
                website VARCHAR(100),
                phone VARCHAR(20),
                picture VARCHAR,
                stars INTEGER,
                location INTEGER
            )
AS
$$
BEGIN
    RETURN QUERY SELECT H.HOT_ID,
                        H.HOT_NAME,
                        H.hot_room_qty,
                        H.hot_room_capacity,
                        H.hot_is_active,
                        H.hot_address_specs,
                        H.hot_room_price,
                        H.hot_website,
                        H.hot_phone,
                        H.HOT_PICTURE,
                        H.HOT_STARS,
                        H.hot_loc_fk
                 FROM Hotel AS H
                 WHERE H.hot_id = p_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetHotelsByCity(city_id integer)
    RETURNS TABLE
            (
                id integer,
                name VARCHAR(100),
                roomQuantity INTEGER,
                roomCapacity INTEGER,
                isActive BOOLEAN,
                addressSpecs VARCHAR(200),
                pricePerRoom DECIMAL,
                website VARCHAR(100),
                phone VARCHAR(20),
                picture VARCHAR,
                stars INTEGER,
                location INTEGER
            )
AS
$$
BEGIN
    RETURN QUERY SELECT H.HOT_ID,
                        H.HOT_NAME,
                        H.hot_room_qty,
                        H.hot_room_capacity,
                        H.hot_is_active,
                        H.hot_address_specs,
                        H.hot_room_price,
                        H.hot_website,
                        H.hot_phone,
                        H.HOT_PICTURE,
                        H.HOT_STARS,
                        H.hot_loc_fk
                 FROM Hotel AS H, LOCATION L
                 WHERE H.hot_loc_fk = L.LOC_ID and H.hot_loc_fk = city_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION UpdateHotel(_id INTEGER,
                                       _name VARCHAR(100),
                                       _amountOfRooms INTEGER,
                                       _capacityPerRoom INTEGER,
                                       _active BOOLEAN,
                                       _addressSpecs VARCHAR(200),
                                       _roomPrice DECIMAL,
                                       _website VARCHAR,
                                       _phone VARCHAR,
                                       _picture VARCHAR,
                                       _stars INTEGER,
                                       _location INTEGER)
    RETURNS TABLE
            (
                id integer,
                name VARCHAR(100),
                roomQuantity INTEGER,
                roomCapacity INTEGER,
                isActive BOOLEAN,
                addressSpecs VARCHAR(200),
                pricePerRoom DECIMAL,
                website VARCHAR(100),
                phone VARCHAR(20),
                picture VARCHAR,
                stars INTEGER,
                location INTEGER
            )
AS
$$
BEGIN
    UPDATE hotel
    SET hot_name          = _name,
        hot_room_qty      = _amountOfRooms,
        hot_room_capacity = _capacityPerRoom,
        hot_is_active     = _active,
        hot_address_specs = _addressSpecs,
        hot_room_price    = _roomPrice,
        hot_website       = _website,
        hot_phone         = _phone,
        hot_picture       = _picture,
        hot_stars         = _stars,
        hot_loc_fk        = _location
    WHERE hot_id = _id;
    return query select * from gethotelbyid(_id);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetLocationById(p_id INTEGER)
    RETURNS TABLE
            (
                id integer,
                city VARCHAR(30),
                country VARCHAR(30)
            )
AS
$$
BEGIN
    RETURN QUERY SELECT LOC_ID, LOC_CITY, LOC_COUNTRY
                 FROM LOCATION
                 WHERE loc_id = p_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetCountries()
    RETURNS TABLE
            (
                id integer,
                city VARCHAR(30),
                country VARCHAR(30)
            )
AS
$$
BEGIN
    RETURN QUERY SELECT X.LOC_ID, X.LOC_CITY, X.LOC_COUNTRY
                 FROM (
                     SELECT row_number() OVER (PARTITION BY LOC_COUNTRY ORDER BY LOC_ID) AS R, T.* FROM LOCATION T
                      ) X
                 WHERE X.R <= 1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetCitiesByCountry(city_id integer)
    RETURNS TABLE
            (
                id integer,
                city VARCHAR(30),
                country VARCHAR(30)
            )
AS
$$
BEGIN
    RETURN QUERY SELECT L.LOC_ID, L.LOC_CITY, L.LOC_COUNTRY
                 FROM LOCATION L, (select LOC_COUNTRY FROM LOCATION WHERE LOC_ID = city_id) AS L1
                 WHERE L.LOC_COUNTRY = L1.LOC_COUNTRY;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION AddLocation(city VARCHAR(30),
                                        country VARCHAR(30))
    RETURNS integer AS
$$
DECLARE
    dest_id INTEGER;
BEGIN
    INSERT INTO LOCATION (loc_city, loc_country) VALUES (city,country) RETURNING LOC_ID INTO dest_id;
    RETURN dest_id;
END;
$$ LANGUAGE plpgsql;

---DELETE HOTEL---
CREATE OR REPLACE FUNCTION DeleteHotel(_HOT_ID integer)
RETURNS integer AS
$$
DECLARE
 RET_ID INTEGER;
BEGIN

    DELETE FROM HOTEL 
    WHERE (HOT_ID = _HOT_ID)
    returning HOT_ID into RET_ID;
   return RET_ID;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION DeleteLocation(_id integer)
    RETURNS integer AS
$$
DECLARE
    RET_ID INTEGER;
BEGIN

    DELETE
    FROM location
    WHERE (loc_id = _id) returning loc_id into RET_ID;
    return RET_ID;
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
DECLARE
 ret_id INTEGER;
BEGIN
	INSERT INTO Ship(shi_id, shi_name, shi_capacity ,shi_loadingcap, shi_model,
                    shi_line, shi_picture ) VALUES
    (default, _shi_name, _shi_capacity, _shi_loadingcap, _shi_model, _shi_line, _shi_picture ) 
	RETURNING shi_id INTO ret_id;
 RETURN ret_id;
END;
$$ LANGUAGE plpgsql;

-------Agregar Crucero(ruta)--------------------

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
DECLARE
  ret_id INTEGER;
BEGIN

   INSERT INTO Cruise(cru_id, cru_shi_fk, cru_departuredate, cru_arrivaldate, cru_price,
                       cru_loc_arrival, cru_loc_departure ) VALUES
    (default, _cru_shi_fk, _cru_departuredate, _cru_arrivaldate, _cru_price, _cru_loc_arrival, _cru_loc_departure )
    RETURNING cru_id INTO ret_id;
   RETURN ret_id;
END;
$$ LANGUAGE plpgsql;

--------Eliminar Ship--------------------
CREATE OR REPLACE FUNCTION DeleteShip(_shi_id integer)
RETURNS integer AS
$$
DECLARE
 ret_id INTEGER;
BEGIN

    DELETE FROM Ship 
    WHERE (shi_id = _shi_id)
    returning shi_id into ret_id;
   return ret_id;
END;
$$ LANGUAGE plpgsql;
--------Eliminar Cruise------------------
--elimina una ruta dado su id
CREATE OR REPLACE FUNCTION DeleteCruise(_cru_id integer)
RETURNS integer AS
$$
DECLARE
  ret_id INTEGER;
BEGIN

    DELETE FROM Cruise 
    WHERE (cru_id = _cru_id)
    returning cru_id into ret_id;
  return ret_id;
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
    RETURNS integer as
$$
declare 
    ret_id integer;
BEGIN

   UPDATE Ship SET shi_isactive = _shi_isactive,
    shi_name = _shi_name, shi_capacity = _shi_capacity,
    shi_loadingcap = _shi_loadingcap, shi_model = _shi_model,
    shi_line = _shi_line, shi_picture = _shi_picture
    WHERE (shi_id = _shi_id)
   returning shi_id into ret_id;
   return ret_id;
END;
$$ LANGUAGE plpgsql;


--------Modificar Cruise(ruta)-----------------

CREATE OR REPLACE FUNCTION ModifyCruise( 
    _cru_id integer,
    _shi_id integer,
    _cru_departuredate TIMESTAMP,
    _cru_arivaldate TIMESTAMP,
    _loc_arrival integer,
    _loc_departure integer,
    _cru_price DECIMAL
    )
RETURNS integer AS
$$
declare
    ret_id integer;
BEGIN

   UPDATE Cruise SET cru_departuredate= _cru_departuredate,
   cru_shi_fk = _shi_id, cru_arrivaldate = _cru_arivaldate, 
   cru_loc_arrival = _loc_arrival, cru_loc_departure = _loc_departure, 
   cru_price = _cru_price
    WHERE (cru_id = _cru_id)
   returning cru_id into ret_id;
   RETURN _cru_id;
END;
$$ LANGUAGE plpgsql;


--------Consultar Ship-------------------
CREATE OR REPLACE FUNCTION GetShip(_shi_id integer)
RETURNS TABLE
  (id integer,
   name VARCHAR(30),
   status boolean,
   capacity_people integer,
   capacity_tonnes integer,
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
--devuelve una tabla con los datos de una ruta dado su id

CREATE OR REPLACE FUNCTION GetCruisers(ship_id integer)
RETURNS TABLE
  (id integer,
   ship integer,
   departure_date TIMESTAMP,
   arrival_date TIMESTAMP,
   price DECIMAL,
   arrival_loc integer,
   departure_loc integer
  )
AS
$$
BEGIN 
    RETURN QUERY SELECT
    cru_id, cru_shi_fk, cru_departuredate, cru_arrivaldate, cru_price, cru_loc_departure, cru_loc_arrival
    FROM Cruise 
    WHERE cru_shi_fk = ship_id;

    
END;
$$ LANGUAGE plpgsql;

-- CREATE OR REPLACE FUNCTION GetCruiseByLocation(_cru_id integer)
-- RETURNS TABLE
--   (id integer,
--    ship VARCHAR(30),
--    departure_date TIMESTAMP,
--    arrival_date TIMESTAMP,
--    price DECIMAL
--   )
-- AS
-- $$
-- BEGIN
--     RETURN QUERY SELECT
--     c.cru_id, s.shi_name, c.cru_departuredate, c.cru_arrivaldate, c.cru_price
--     FROM Cruise c, Ship s WHERE c.cru_id = _cru_id and s.shi_id = c.cru_shi_fk;
-- END;
-- $$ LANGUAGE plpgsql;
---------Retorna todos los Cruceros--------
CREATE OR REPLACE FUNCTION GetAllShip()
RETURNS TABLE
  (id integer,
   name VARCHAR(30),
   status boolean,
   capacity_people integer,
   capacity_tonnes integer,
   model VARCHAR(30),
   cruise_line VARCHAR(30),
   picture VARCHAR
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    shi_id, shi_name, shi_isactive, shi_capacity, shi_loadingcap, shi_model, shi_line,shi_picture
    FROM Ship;
END;
$$ LANGUAGE plpgsql;
------------------------------------fin de grupo 8---------------------------------


------------------------------------ grupo 9 ----------------------------------------

--------------------------------------AGREGAR RECLAMO----------------------------------
CREATE OR REPLACE FUNCTION AddClaim(
    _cla_title VARCHAR(20), 
    _cla_description VARCHAR(30),
	  _bag_id int
    ) 
RETURNS integer AS
$$
DECLARE
    _cla_ID INTEGER;
BEGIN

   INSERT INTO Claim(cla_title, cla_descr, cla_status) VALUES
   ( _cla_title, _cla_description, 'ABIERTO')RETURNING cla_ID INTO _cla_ID;
	
    if (_cla_ID is not null)then 
	    update BAGGAGE set bag_status = 'EXTRAVIADO' , bag_cla_fk= _cla_id where bag_id = _bag_id;
	  end if;
   RETURN _cla_ID;
END;
$$ LANGUAGE plpgsql;

----------------------------------------MODIFICAR RECLAMO -----------------------------
CREATE OR REPLACE FUNCTION ModifyClaimStatus( 
    _cla_id integer,
    _cla_status VARCHAR(35))
RETURNS integer AS
$$
BEGIN
	if(_cla_status ='CERRADO') then
	  UPDATE BAGGAGE set bag_status='ENCONTRADO' where bag_cla_fk= _cla_id;
	end if;
	if(_cla_status = 'ABIERTO') then
	  UPDATE BAGGAGE set bag_status='EXTRAVIADO' where bag_cla_fk= _cla_id;
	end if;
  UPDATE Claim SET cla_status= _cla_status
  WHERE (cla_id = _cla_id);
  RETURN _cla_id;
END;
$$ LANGUAGE plpgsql;

----------------------- MODIFICAR TITUO Y DESCRIPCION DEL RECLAMO --------------------
CREATE OR REPLACE FUNCTION ModifyClaimTitle( 
	_cla_id integer,
  _cla_title VARCHAR(35),
	_cla_descr VARCHAR(35))
RETURNS integer AS
$$
BEGIN

  UPDATE Claim SET cla_title= _cla_title, cla_descr= _cla_descr
	WHERE (cla_id = _cla_id);
  RETURN _cla_id;
END;
$$ LANGUAGE plpgsql;

-------------------------------------ELIMAR RECLAMO-----------------------------
CREATE OR REPLACE FUNCTION DeleteClaim(_cla_id integer)
RETURNS integer AS
$$
BEGIN
	Update BAGGAGE set bag_cla_fk= null where bag_cla_fk=_cla_id;
  
  DELETE FROM Claim 
  WHERE (cla_id = _cla_id);
  
  RETURN _cla_id;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR RECLAMO ------------------------------------
CREATE OR REPLACE FUNCTION GetClaim(_cla_id integer)
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30),
   idEquipaje integer
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cla_id, cla_title,cla_descr, cla_status ,bag_id
    FROM Claim, baggage WHERE cla_id = _cla_id and cla_id= bag_cla_fk ;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR RECLAMO POR DOCUMENTO -------------------------
CREATE OR REPLACE FUNCTION GetClaimDocument(_users_document_id varchar(30))
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30),
   idEquipaje integer
  )
AS
$$
BEGIN    
    RETURN QUERY SELECT 
    cla_id, cla_title,cla_descr, cla_status , bag_id from claim, Baggage , res_fli, users
    where bag_res_fli_fk =rf_id and rf_use_fk =use_id  and use_document_id=_users_document_id
    and bag_cla_fk=cla_id; 
END;
$$ LANGUAGE plpgsql;

---------------------- CONSULTAR RECLAMO SEGUN ESTATUS -------------------------
CREATE OR REPLACE FUNCTION GetClaimStatus(_cla_status varchar(30))
RETURNS TABLE
  (id integer,
   title VARCHAR(30),
   descr VARCHAR(30),
   status VARCHAR(30),
   idEquipaje integer
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    cla_id, cla_title,cla_descr, cla_status, bag_id
    FROM Claim, baggage WHERE cla_status = _cla_status and cla_id= bag_cla_fk ;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR EQUIPAJE POR SERIAL-------------------------
CREATE OR REPLACE FUNCTION GetBaggage(_BAG_ID integer)
RETURNS TABLE
  (id integer,
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    BAG_ID, bag_descr, bag_status
    FROM BAGGAGE WHERE BAG_ID = _BAG_ID;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR EQUIPAJE POR DOCUMENTO -------------------------
CREATE OR REPLACE FUNCTION GetBaggageDocumentPasaport(_users_document_id varchar(30))
RETURNS TABLE
  (id integer,
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN    
    RETURN QUERY SELECT 
    bag_id, bag_descr, bag_status from Baggage , res_fli, users
    where bag_res_fli_fk =rf_id and rf_use_fk =use_id  and use_document_id=_users_document_id;
END;
$$ LANGUAGE plpgsql;

------------------- CONSUTAR EQUIPAJE SEGUN ESTATUS------------------------------------
CREATE OR REPLACE FUNCTION GetBaggageStatus(_bag_status varchar(30))
RETURNS TABLE
  (id integer,
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    BAG_id, BAG_descr, BAG_status
    FROM Baggage WHERE BAG_status = _bag_status;
END;
$$ LANGUAGE plpgsql;

---------------------------- MODIFICAR ESTATUS DE EQUIPAJE -----------------------
CREATE OR REPLACE FUNCTION modifyBaggagestatus( 
    _bag_id integer,
    _bag_status VARCHAR(30))
RETURNS integer AS
$$
DECLARE
    _cla_ID INTEGER;
BEGIN
	if(_bag_status = 'RECLAMADO') then
   UPDATE Baggage SET bag_status = _bag_status 
   WHERE (bag_id = _BAG_ID) RETURNING bag_cla_fk INTO _cla_ID;

   if(_cla_ID is not null) then
         UPDATE Baggage SET bag_cla_fk = null 
         WHERE (bag_id = _BAG_ID);
  
         delete FROM CLAIM where cla_ID= _cla_ID;
   end if;
   RETURN _BAG_ID;
  end if;

END;
$$ LANGUAGE plpgsql;

------------------------------------FIN DEL GRUPO 9--------------------------------

-----------------------------------Grupo 5 ------------------------------------------------
-------------AGREGAR AUTO-----------------

CREATE OR REPLACE FUNCTION 
ADDAUTOMOBILE(
    _make VARCHAR(20), 
    _model VARCHAR(30),
    _capacity integer,
    _status BOOLEAN,
    _licence varchar(30),
    _price real,
    _picture varchar,
    _place integer
    ) 
RETURNS integer AS
$$

BEGIN


   INSERT INTO AUTOMOBILE(AUT_MAKE,AUT_MODEL,AUT_CAPACITY,AUT_ISACTIVE,AUT_LICENSE,AUT_PRICE,AUT_PICTURE,AUT_LOC_FK) VALUES
    ( _make, _model,_capacity,_status,_licence,_price,_picture,_place);
   RETURN 1 ;
END;
$$ LANGUAGE plpgsql;


----------------eliminar auto-------------------------------

CREATE OR REPLACE FUNCTION DeleteAuto(_id integer)
RETURNS void AS
$$
BEGIN

    DELETE FROM AUTOMOBILE
    WHERE (AUT_ID = _id);

END;
$$ LANGUAGE plpgsql;
-------consultar por id de auto-------------------------
CREATE OR REPLACE FUNCTION ConsultforIdAuto(codigo integer)
RETURNS TABLE
  (id integer,
   make varchar(30),
   model varchar(30),
   capacity integer,
   isactive BOOLEAN, 
   price numeric , 
   license varchar(30), 
   picture varchar (30), 
   loc_fk integer
  )
AS
$$
BEGIN
    RETURN QUERY  select * 
    FROM AUTOMOBILE  WHERE aut_id = codigo;
END;
$$ LANGUAGE plpgsql;

----------consultar por lugar ----------------------------------
CREATE OR REPLACE FUNCTION ConsultforPlaceAuto(codigo integer)
RETURNS TABLE
  (id integer,
   make varchar(30),
   model varchar(30),
   capacity integer,
   isactive BOOLEAN, 
   price numeric , 
   license varchar(30), 
   picture varchar (30), 
   loc_fk integer
  )
AS
$$
BEGIN
    RETURN QUERY  select * 
    FROM AUTOMOBILE  WHERE aut_loc_fk = codigo ;
END;
$$ LANGUAGE plpgsql;

--------------------------------------------------
CREATE OR REPLACE FUNCTION ConsultforPlaceandStatusAuto(_place integer,_status bool )
RETURNS TABLE
  (id integer,
   make varchar(30),
   model varchar(30),
   capacity integer,
   isactive BOOLEAN, 
   price numeric , 
   license varchar(30), 
   picture varchar (30), 
   loc_fk integer
  )
AS
$$
BEGIN
    RETURN QUERY  select * 
    FROM AUTOMOBILE  WHERE aut_loc_fk = _place and aut_isactive =_status ;
END;
$$ LANGUAGE plpgsql;

-------------consultar por estado del auto ---------------------------------
CREATE OR REPLACE FUNCTION ConsultforStatusAuto(codigo integer)
RETURNS TABLE
  (id integer,
   make varchar(30),
   model varchar(30),
   capacity integer,
   isactive BOOLEAN, 
   price real , 
   license varchar(30), 
   picture varchar (30), 
   loc_fk integer
  )
AS
$$
BEGIN
    RETURN QUERY  select * 
    FROM AUTOMOBILE  WHERE aut_isactive = codigo ;
END;
$$ LANGUAGE plpgsql;
-------------------consulta por marca de auto-------------------------------
CREATE OR REPLACE FUNCTION ConsultforMakeAuto(codigo integer)
RETURNS TABLE
  (id integer,
   make varchar(30),
   model varchar(30),
   capacity integer,
   isactive BOOLEAN, 
   price real , 
   license varchar(30), 
   picture varchar (30), 
   loc_fk integer
  )
AS
$$
BEGIN
    RETURN QUERY  select * 
    FROM AUTOMOBILE  WHERE aut_make = codigo ;
END;
$$ LANGUAGE plpgsql;
------------------consulta por modelo----------------------------------------
CREATE OR REPLACE FUNCTION ConsultformodelAuto(codigo integer)
RETURNS TABLE
  (id integer,
   make varchar(30),
   model varchar(30),
   capacity integer,
   isactive BOOLEAN, 
   price real , 
   license varchar(30), 
   picture varchar (30), 
   loc_fk integer
  )
AS
$$
BEGIN
    RETURN QUERY  select * 
    FROM AUTOMOBILE  WHERE aut_model = codigo ;
END;
$$ LANGUAGE plpgsql;
----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION consultayuda(
	_place integer,
	_result varchar,
	_license character varying,
	_capacity integer)
    RETURNS TABLE(id integer, make character varying, model character varying, capacity integer, isactive boolean, price numeric, license character varying, picture character varying, loc_fk integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$
declare
    _status bool;
BEGIN
   
	if (_result = 'true'  ) then 
		_status:=true;
	else  
	    _status:= false;
    end if;
	IF (_result = 'null' and _license = 'null' and _capacity= 0 ) THEN
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk = _place;
    ELSIF  (_place = 0  and _license = 'null' and _capacity= 0 ) THEN
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_isactive = _status ;
	ELSIF (_place = 0  and _result = 'null' and _capacity= 0 ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_license = _license ;
	ELSIF (_place = 0  and _result = 'null' and _license= 'null' ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_capacity = _capacity ;
	ELSIF (_place = 0 and _result= 'null') then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_license = _license  and aut_capacity = _capacity;
	ELSIF (_place = 0 and _license='null') then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_isactive = _status  and aut_capacity = _capacity;
	ELSIF (_place=0 and _capacity = 0) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_isactive = _status  and aut_license = _license;
    ELSIF (_place=0 ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_isactive = _status  and aut_license = _license and aut_capacity =_capacity;
    ELSIF (_result= 'null' and _license ='null' ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk=_place and aut_capacity =_capacity;
    ELSIF (_result= 'null' and _capacity =0 ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk=_place and aut_license =_license;
    ELSIF (_result= 'null' ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk=_place and aut_license =_license and aut_capacity= _capacity;		
    ELSIF (_license = 'null' and _capacity= 0  ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk=_place  and aut_isactive= _status;	
    ELSIF ( _capacity= 0  ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk=_place  and aut_isactive= _status and aut_license=_license;	
    ELSIF (_license = 'null'  ) then 
		RETURN QUERY  select * FROM AUTOMOBILE  WHERE aut_loc_fk=_place  and aut_isactive= _status and aut_capacity = _capacity;
	ELSE 
		RETURN QUERY  select *FROM AUTOMOBILE where aut_loc_fk=_place  and aut_isactive= _status and aut_capacity = _capacity and aut_license=_license;
	END IF; 

END;
$BODY$;


-------------modificar auto--------------------------------------------------
CREATE OR REPLACE FUNCTION 
MODIFYAUTOMOBILE(
	_id integer,
    _make VARCHAR(20), 
    _model VARCHAR(30),
    _capacity integer,
    _status BOOLEAN,
    _license varchar(30),
    _price real,
    _picture varchar,
    _place integer
    ) 
RETURNS integer AS
$$
BEGIN
UPDATE automobile
	SET  aut_make=_make, aut_model=_model,
	aut_capacity=_capacity, aut_isactive=_status, aut_price=_price,
	aut_license=_license, aut_picture=_picture, aut_loc_fk=_place
	WHERE aut_id =_id;
	RETURN _id;
END;
$$ LANGUAGE plpgsql;
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
----------------------------------------- consulta de ciudades-----------------------------------

CREATE OR REPLACE FUNCTION GetCity()
    RETURNS TABLE
            (
                id integer,
                city VARCHAR(30)
               
            )
AS
$$
BEGIN
    RETURN QUERY select loc_id,loc_city from location;
END;
$$ LANGUAGE plpgsql;

------------------------fin de grupo 5-------------------------------------------

------------------------------------inicio de grupo 7---------------------------------

CREATE OR REPLACE FUNCTION GetRestaurant(_res_id integer)
RETURNS TABLE
  (id integer,
   name VARCHAR(100),
   capacity INTEGER,
   isActive BOOLEAN,
   qualify DECIMAL,
   specialty VARCHAR(30),
   price DECIMAL,
   businessName VARCHAR(30),
   picture VARCHAR,
   description VARCHAR(30),
   phone VARCHAR(30),
   location INTEGER,
   address VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    R.res_id, R.res_name, R.res_capacity , R.res_isactive, R.res_qualify ,R.res_specialty,R.res_price, R.res_businessname, R.res_picture, R.res_descr, R.res_tlf, R.res_loc_fk, R.res_address_specs
    FROM Restaurant AS R WHERE R.res_id = _res_id;
END;
$$ LANGUAGE plpgsql;

--------------------------CONSULTAR Restaurants--------------------

CREATE OR REPLACE FUNCTION GetRestaurants()
RETURNS TABLE
  (id integer,
   name VARCHAR(100),
   capacity INTEGER,
   isActive BOOLEAN,
   qualify DECIMAL,
   specialty VARCHAR(30),
   price DECIMAL,
   businessName VARCHAR(30),
   picture VARCHAR,
   description VARCHAR(30),
   phone VARCHAR(30),
   location INTEGER,
   address VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    R.res_id, R.res_name, R.res_capacity , R.res_isactive, R.res_qualify ,R.res_specialty,R.res_price, R.res_businessname, R.res_picture, R.res_descr, R.res_tlf, R.res_loc_fk, R.res_address_specs
    FROM Restaurant AS R;
END;
$$ LANGUAGE plpgsql;

--------------------------Agregar Restaurant--------------------

CREATE OR REPLACE FUNCTION AddRestaurant(name VARCHAR(100),
                                    capacity INTEGER,
                                    isActive BOOLEAN,
									qualify DECIMAL,
                                    specialty VARCHAR(30),
                                    price DECIMAL,
									businessName VARCHAR(30),
									picture VARCHAR,
									description VARCHAR(30),
									phone VARCHAR(30),
                                    location INTEGER,
									address VARCHAR(30))
    RETURNS integer AS
$$
DECLARE
    DEST_ID INTEGER;
BEGIN
    INSERT INTO restaurant (RES_NAME,RES_CAPACITY,RES_ISACTIVE,RES_QUALIFY,RES_SPECIALTY,RES_PRICE,
  RES_BUSINESSNAME,RES_PICTURE,RES_DESCR,RES_TLF,RES_LOC_FK,RES_ADDRESS_SPECS)
    VALUES (name,capacity,isActive,qualify,specialty,price,businessName,
			picture,description,phone,location,address) RETURNING RES_ID INTO DEST_ID;
    RETURN DEST_ID;
END;
$$ LANGUAGE plpgsql; 

--------------------------Consultar restaurants por ciudad--------------------

CREATE OR REPLACE FUNCTION GetRestaurantsByCity(sent_location integer)
RETURNS TABLE
  (id integer,
   name VARCHAR(100),
   capacity INTEGER,
   isActive BOOLEAN,
   qualify DECIMAL,
   specialty VARCHAR(30),
   price DECIMAL,
   businessName VARCHAR(30),
   picture VARCHAR,
   description VARCHAR(30),
   phone VARCHAR(30),
   location INTEGER,
   address VARCHAR(30)
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    R.res_id, R.res_name, R.res_capacity , R.res_isactive, R.res_qualify ,R.res_specialty,R.res_price, R.res_businessname, R.res_picture, R.res_descr, R.res_tlf, R.res_loc_fk, R.res_address_specs
    FROM Restaurant AS R
	WHERE R.res_loc_fk = sent_location;
END;
$$ LANGUAGE plpgsql;

--------------------------Borrar Restaurant--------------------

CREATE OR REPLACE FUNCTION DeleteRestaurant(id integer)
RETURNS INTEGER AS
$$
DECLARE
    FOUND_ID INTEGER;
BEGIN
	SELECT Count(res_id) into FOUND_ID 
	FROM Restaurant 
	WHERE (res_id = id);
	IF (FOUND_ID = 0) THEN
		RETURN null;
	END IF;
	
    DELETE FROM Restaurant 
    WHERE (res_id = id);
	RETURN id;

END;
$$ LANGUAGE plpgsql;

--------------------------Actualizar Restaurant--------------------

CREATE OR REPLACE FUNCTION ModifyRestaurant( 
   id integer,
   name VARCHAR(100),
   capacity INTEGER,
   isActive BOOLEAN,
   qualify DECIMAL,
   specialty VARCHAR(30),
   price DECIMAL,
   businessName VARCHAR(30),
   picture VARCHAR,
   description VARCHAR(30),
   phone VARCHAR(30),
   location INTEGER,
   address VARCHAR(30))
RETURNS integer AS
$$
BEGIN

   UPDATE Restaurant SET res_name = name, 
   res_capacity = capacity, res_isactive = isActive, res_qualify = qualify ,
   res_specialty = specialty, res_price = price, res_businessname = businessName, 
   res_picture = picture, res_descr = description, res_tlf = phone, res_loc_fk = location, 
   res_address_specs = address
    WHERE (res_id = id);
   RETURN id;
END;
$$ LANGUAGE plpgsql;

------------------------------------fin de grupo 7---------------------------------

------------------------------------ grupo 10 ---------------------------------

-- DROP FUNCTION GetTravels(BIGINT);
CREATE OR REPLACE FUNCTION GetTravels(userId BIGINT) 
RETURNS TABLE (
	travel_id INTEGER,
	travel_name VARCHAR,
	travel_init DATE,
	travel_end DATE,
	travel_description VARCHAR,
  travel_userId INTEGER
) AS $$
BEGIN
	RETURN QUERY 
	SELECT tra_id, tra_name, tra_ini, tra_end, tra_descr, tra_use_fk FROM travel WHERE tra_use_fk = userId;
END; $$ 
LANGUAGE plpgsql;

-- DROP FUNCTION GetLocationsByTravel(BIGINT);
CREATE OR REPLACE FUNCTION GetLocationsByTravel(travelId BIGINT)
RETURNS TABLE (
	locationId INTEGER, 
	locationCity VARCHAR
) AS $$
BEGIN
RETURN QUERY
	SELECT TL.tl_loc_fk, L.loc_city
	FROM TRA_LOC TL
	INNER JOIN public.LOCATION L ON TL.tl_loc_fk = L.loc_id
	WHERE TL.tl_tra_fk = travelId; 
END; $$
LANGUAGE plpgsql;  

CREATE OR REPLACE FUNCTION AddTravel(
	travelName VARCHAR,  
	travelInit VARCHAR,
	travelEnd VARCHAR,
  travelDescription VARCHAR,
	userId BIGINT)
RETURNS BIGINT AS
$$
DECLARE
	travelId BIGINT;
BEGIN
	INSERT INTO Travel(tra_name, tra_ini, tra_end, tra_descr, tra_use_fk)
	VALUES(travelName, to_date(travelInit,'YYYY-MM-DD'), to_date(travelEnd,'YYYY-MM-DD'), travelDescription, userId) RETURNING tra_id INTO travelId;
	RETURN travelId;
END;
$$
LANGUAGE 'plpgsql';


------------------------------------fin de grupo 10---------------------------------

----------------------------------- grupo 14 ---------------------------------

--SP para insertar una reserva de hotel
CREATE OR REPLACE FUNCTION addReservationRestaurant(fecha VARCHAR, 
    people INTEGER, fecha_reservacion VARCHAR, userId INTEGER, restaurantId INTEGER)
  RETURNS INTEGER AS $$
  DECLARE 
    res_rest_id INTEGER;
  
  BEGIN
    INSERT INTO res_rest(rr_date, RR_NUM_PPL, rr_timestamp, rr_use_fk,
      rr_res_fk)
      VALUES (TO_TIMESTAMP(fecha, 'yyyy-mm-dd hh24:mi'), people, TO_TIMESTAMP(fecha_reservacion,'yyyy-mm-dd hh24:mi'),userId,restaurantId) RETURNING rr_id INTO res_rest_id;
    
    RETURN res_rest_id;
  END;
  $$ LANGUAGE plpgsql;

--SP para retornar todas las reservas de restaurant para un usuario
CREATE OR REPLACE FUNCTION getResRestaurant(usuario INTEGER) RETURNS TABLE (
  id INTEGER, fecha_for_res TIMESTAMP, number_people INTEGER, fecha_que_reservo TIMESTAMP,
  userID INTEGER, restaurantID INTEGER, paymentID INTEGER) AS $$
  
  BEGIN
    RETURN QUERY SELECT R.rr_id as ID, R.rr_date as fechaReservar, R.RR_NUM_PPL as CantidadPersonas, R.rr_timestamp as fechaReservo,
    R.rr_use_fk as userID, R.rr_res_fk as restaurantID, R.rr_pay_fk as paymentID
    FROM RES_REST as R, users as U
    WHERE U.USE_ID = R.rr_use_fk and R.rr_use_fk = usuario;
  END;
  $$ LANGUAGE plpgsql;

--SP para cancelar la reserva de un usuario 
CREATE OR  REPLACE FUNCTION deleteReservation(reservationID INTEGER) RETURNS INTEGER AS $$
  DECLARE res_id INTEGER;
  BEGIN
    DELETE FROM RES_REST WHERE (rr_id = reservationID) returning rr_id INTO res_id;

    RETURN res_id;
  END;
  $$ LANGUAGE plpgsql;

--SP para agregar si se pago la reserva
CREATE OR REPLACE FUNCTION modifyReservationPayment(pay INTEGER,reservation INTEGER) RETURNS INTEGER AS $$
  DECLARE res_id INTEGER;
  BEGIN
    UPDATE res_rest SET rr_pay_fk = pay 
    WHERE(rr_id = reservation) returning rr_id INTO res_id;
    RETURN res_id;
  END;
  $$ LANGUAGE plpgsql;

--SP para traerme la cantidad de personas que ya han reservado en el restaurant seleccionado
CREATE OR REPLACE FUNCTION getAvailability(_res_id INTEGER, _res_date VARCHAR) RETURNS INTEGER AS $$
  DECLARE 
  available INTEGER;
  total INTEGER;
  capacity INTEGER;
  BEGIN
    SELECT SUM(rr_num_ppl) FROM RES_REST 
    WHERE rr_res_fk = _res_id
    AND rr_date = TO_TIMESTAMP(_res_date, 'yyyy-mm-dd hh24:mi')
    INTO available;

    SELECT res_capacity FROM RESTAURANT
    WHERE RES_ID = _res_id
    INTO capacity;
    
    total = capacity - available;
	  IF total > 0 THEN 
		  RAISE NOTICE 'dentro del if';
		  RETURN total;
	  END IF;
	  RETURN 0;
  END;
  $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION getAvailabilityRest(_res_id INTEGER) RETURNS INTEGER AS $$
  DECLARE capaityRest INTEGER;
  BEGIN
    SELECT res_capacity FROM RESTAURANT
    WHERE RES_ID = _res_id
    INTO capaityRest;

    RETURN capaityRest;
  END;
  $$ LANGUAGE plpgsql;
-----------------------------------fin grupo 14-----------------------------------------------------------

------Grupo1-----------------------------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION LoginRepository(Email varchar(20),Password VARCHAR(50)) RETURNS table (use_id integer,use_name varchar(50),use_last_name varchar(30),usr_rol_id integer,rol_name varchar(30))AS $BODY$
        BEGIN
		RETURN QUERY
                select USERS.use_id,USERS.use_name,USERS.use_last_name,User_Role.usr_rol_id,Role.rol_name from USERS,User_Role,Role WHERE USERS.use_email=$1 and (USERS.use_password=MD5($2) or 
				USERS.use_password=$2) and USERS.use_id=User_Role.usr_use_id and
				Role.rol_id=User_Role.usr_rol_id;
        END;
$BODY$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION RecoveryPass(Email varchar(20)) RETURNS table (use_name varchar(50),use_lastname varchar(30),use_password varchar(50))AS $BODY$
        BEGIN
		UPDATE Users set use_password=(SELECT md5(random()::text)) where USERS.use_email=$1;
		RETURN QUERY
          select USERS.use_name,USERS.use_last_name,USERS.use_password from USERS WHERE USERS.use_email=$1 ;
        END
$BODY$ LANGUAGE plpgsql;sos
---------------------------------finGrupo1---------------------------------------------------------------------------
