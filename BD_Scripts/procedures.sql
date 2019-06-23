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
	FROM public.Flight WHERE fli_departuredate BETWEEN TO_TIMESTAMP(_begin,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone AND TO_TIMESTAMP(_end,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone + '1 days'::interval;
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
	FROM public.Flight WHERE fli_departuredate::date = TO_TIMESTAMP(_departuredate,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone and fli_arrivaldate::date = TO_TIMESTAMP(_arrivaldate,'MM-DD-YYYY HH24:MI:SS')::timestamp without time zone
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

CREATE OR REPLACE FUNCTION GetUserByEmail(email_id VARCHAR(30), user_id INTEGER)
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
               FROM Users WHERE use_email = email_id AND use_id <> user_id;
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

------grupo4---------------
---------------------------Agregar Baggage-------------------------------

CREATE OR REPLACE FUNCTION AddBaggage(
	_bag_res_fli_fk INTEGER,
    _bag_res_cru_fk INTEGER,
    _bag_descr VARCHAR(100),
    _bag_status VARCHAR(100))
  RETURNS INTEGER AS
$$
DECLARE
  ret_id INTEGER;
BEGIN
    INSERT INTO Baggage(bag_id, bag_res_fli_fk, bag_res_cru_fk, bag_descr, bag_status) VALUES
        (default, _bag_res_fli_fk, _bag_res_cru_fk, _bag_descr, _bag_status)
    RETURNING bag_id INTO ret_id;
RETURN ret_id;
END;
$$ LANGUAGE plpgsql;

---------------------------Eliminar Baggage-------------------------------
CREATE OR REPLACE FUNCTION DeleteBaggage(_bag_id INTEGER)
RETURNS INTEGER AS
$$
DECLARE
 ret_id INTEGER;
BEGIN

    DELETE FROM Baggage 
    WHERE (bag_id = _bag_id)
    returning bag_id into ret_id;
   return ret_id;
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
   picture text,
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
                        '/hotels/' || H.HOT_ID || '/image' picture,
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
                picture text,
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
                        '/hotels/' || H.HOT_ID || '/image' picture,
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
                picture text,
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
                        '/hotels/' || H.HOT_ID || '/image' picture,
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
                picture text,
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

CREATE OR REPLACE FUNCTION GetHotelImage(p_id INTEGER)
    RETURNS VARCHAR AS
$$
DECLARE
    img VARCHAR;
BEGIN
    SELECT hot_picture
    FROM Hotel
    WHERE hot_id = p_id INTO img;
    return img;
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
-------Update Cruiser Status--------------------
CREATE OR REPLACE FUNCTION UpdateShipStatus(
_shi_id integer,
_status boolean
)
returns integer AS
$$
declare 
ret_id integer;
begin
update ship set shi_isactive = _status where shi_id = _shi_id returning shi_id into ret_id;
return ret_id;
END;
$$ LANGUAGE plpgsql;
-------Agregar Crucero(ruta)--------------------

CREATE OR REPLACE FUNCTION AddCruise( 
  _cru_shi_fk INTEGER,
  _cru_departuredate VARCHAR,
  _cru_arrivaldate VARCHAR,
  _cru_price DECIMAL,
  _cru_loc_departure INTEGER,
  _cru_loc_arrival INTEGER
  ) 
RETURNS integer AS
$$
DECLARE
  ret_id INTEGER;
BEGIN

   INSERT INTO Cruise(cru_id, cru_shi_fk, cru_departuredate, cru_arrivaldate, cru_price,
                       cru_loc_arrival, cru_loc_departure ) VALUES
    (default, _cru_shi_fk, TO_TIMESTAMP(_cru_departuredate, 'yyyy-mm-dd hh24:mi'), TO_TIMESTAMP(_cru_arrivaldate, 'yyyy-mm-dd hh24:mi'), _cru_price, _cru_loc_arrival, _cru_loc_departure )
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
    DELETE FROM Cruise
    WHERE (cru_shi_fk = _shi_id);
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
    _cru_departuredate VARCHAR,
    _cru_arivaldate VARCHAR,
    _loc_departure integer,
    _loc_arrival integer,
    _cru_price DECIMAL
    )
RETURNS TABLE
  (id integer,
   ship integer,
   departure_date TIMESTAMP,
   arrival_date TIMESTAMP,
   price DECIMAL,
   arrival_loc integer,
   departure_loc integer
  )AS
$$
BEGIN
   UPDATE Cruise SET cru_departuredate= TO_TIMESTAMP(_cru_departuredate, 'yyyy-mm-dd hh24:mi'),
   cru_shi_fk = _shi_id, cru_arrivaldate = TO_TIMESTAMP(_cru_arrivaldate, 'yyyy-mm-dd hh24:mi'), 
   cru_loc_arrival = _loc_arrival, cru_loc_departure = _loc_departure, 
   cru_price = _cru_price
    WHERE (cru_id = _cru_id);
   RETURN query select * from cruise where cru_id = _cru_id;
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
   cruise_line VARCHAR(30),
   picture VARCHAR
  )
AS
$$
BEGIN
    RETURN QUERY SELECT
    shi_id, shi_name, shi_isactive, shi_capacity, shi_loadingcap, shi_model, shi_line,shi_picture
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
--------------get cruise by loc------------------------------------------
CREATE OR REPLACE FUNCTION GetCruiseByLocation(_dep_id integer, _arr_id integer)
RETURNS TABLE
  (id integer,
   ship_id integer,
   departure_date TIMESTAMP,
   arrival_date TIMESTAMP,
   price DECIMAL,
   departure_loc integer,
   arrival_loc integer
  )
AS
$$
DECLARE
  stat boolean;
BEGIN 
  SELECT shi_isactive FROM ship INTO stat 
  WHERE shi_id IN (SELECT cru_shi_fk FROM CRUISE
  WHERE cru_loc_departure = _dep_id
  AND cru_loc_arrival = _arr_id);
  IF stat = True THEN
      RETURN QUERY SELECT
      c.cru_id,c.cru_shi_fk, c.cru_departuredate, c.cru_arrivaldate, c.cru_price,
      c.cru_loc_departure, c.cru_loc_arrival
    FROM Cruise c
    WHERE c.cru_loc_departure = _dep_id
    AND c.cru_loc_arrival = _arr_id;
  END IF;  
END;
$$ LANGUAGE plpgsql;

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
    cla_id, cla_title,cla_descr, cla_status , bag_id from claim, Baggage , res_cru, users
    where  BAG_RES_CRU_FK = rc_id and rc_use_fk =use_id 
    and use_document_id=_users_document_id
    and bag_cla_fk=cla_id 
	UNION
	SELECT 
    cla_id, cla_title,cla_descr, cla_status , bag_id from claim, Baggage , res_fli, users
    where  BAG_RES_fli_FK = rf_id and rf_use_fk =use_id 
    and use_document_id=_users_document_id
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
CREATE OR REPLACE FUNCTION getBaggageDOcumentpasaport(_users_document_id varchar(30))
RETURNS TABLE
  (id integer,
   descr VARCHAR(30),
   status VARCHAR(30)
  )
AS
$$
BEGIN    
    RETURN QUERY SELECT 
    bag_id, bag_descr, bag_status from  Baggage , res_cru, users
    where  BAG_RES_CRU_FK = rc_id and rc_use_fk =use_id 
    and use_document_id=_users_document_id
	UNION
	SELECT 
    bag_id, bag_descr, bag_status from  Baggage , res_fli, users
    where  BAG_RES_fli_FK = rf_id and rf_use_fk =use_id 
    and use_document_id=_users_document_id; 
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
CREATE OR REPLACE FUNCTION AddVehicle( 
	modelId INTEGER,
	locationId INTEGER,
	license VARCHAR,
	price DECIMAL,
	status BOOLEAN
)
RETURNS INTEGER AS
$$
DECLARE
	vehicleId INTEGER;
BEGIN
	INSERT INTO VEHICLE(veh_model, veh_location, veh_license, veh_price, veh_status)
	VALUES(modelId, locationId, license, price, status) RETURNING veh_id INTO vehicleId;
	RETURN vehicleId;
END;
$$
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION AddBrand(  
	brandName VARCHAR)
RETURNS INTEGER AS
$$
DECLARE
	brandId INTEGER;
BEGIN
	INSERT INTO VEH_BRAND(vb_name)
	VALUES(brandName) RETURNING vb_id INTO brandId;
	RETURN brandId;
END;
$$
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION AddModel( 
	brandId INTEGER,
	modelName VARCHAR,
	capacity INTEGER,
	picture VARCHAR
)
RETURNS INTEGER AS
$$
DECLARE
	modelId INTEGER;
BEGIN
	INSERT INTO VEH_MODEL(vm_brand, vm_name, vm_capacity, vm_picture)
	VALUES(brandId, modelName, capacity, picture) RETURNING vm_id INTO modelId;
	RETURN modelId;
END;
$$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION GetVehiclesByLocation(locationId INTEGER) 
RETURNS TABLE (
	vehicleId INTEGER,
	modelId INTEGER,
	location_id INTEGER,
	license VARCHAR,
	price DECIMAL,
	status BOOLEAN
) AS $$
BEGIN
	RETURN QUERY 
	SELECT veh_id, veh_model, veh_location, veh_license, veh_price, veh_status 
	FROM VEHICLE WHERE veh_location = locationId;
END; $$ 
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION GetVehicles() 
RETURNS TABLE (
	vehicleId INTEGER,
	modelId INTEGER,
	locationId INTEGER,
	license VARCHAR,
	price DECIMAL,
	status BOOLEAN
) AS $$
BEGIN
	RETURN QUERY 
	SELECT veh_id, veh_model, veh_location, veh_license, veh_price, veh_status 
	FROM VEHICLE;
END; $$ 
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION GetBrands() 
RETURNS TABLE (
	brandId INTEGER,
	brandName VARCHAR
) AS $$
BEGIN
	RETURN QUERY 
	SELECT vb_id, vb_name FROM VEH_BRAND;
END; $$ 
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION GetModelsByBrand(brandId INTEGER) 
RETURNS TABLE (
	modelId INTEGER,
	brand INTEGER,
	modelName VARCHAR,
	capacity INTEGER,
	picture VARCHAR
) AS $$
BEGIN
	RETURN QUERY 
	SELECT vm_id, vm_brand, vm_name, vm_capacity, vm_picture FROM VEH_MODEL WHERE vm_brand = brandId;
END; $$ 
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION UpdateModel(
  vmid INTEGER, 
  vmbrand INTEGER, 
  vmname VARCHAR, 
  vmcapacity INTEGER, 
  vmpicture VARCHAR
)
RETURNS BOOLEAN AS
$$
BEGIN
  UPDATE veh_model
  SET vm_brand = vmbrand,
  vm_name = vmname, vm_capacity = vmcapacity,vm_picture = vmpicture
  WHERE vm_id = vmid;
  IF FOUND THEN
    RETURN TRUE;
  ELSE
    RETURN FALSE;
  END IF;
END;
$$
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION UpdateBrand(
  vbid INTEGER,
  vbname VARCHAR
)
RETURNS BOOLEAN AS
$$
BEGIN
  UPDATE veh_brand
  SET vb_name = vbname
  WHERE vb_id = vbid;
  IF FOUND THEN
    RETURN TRUE;
  ELSE
    RETURN FALSE;
  END IF;
END;
$$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION UpdateVehicle(
  vehid INTEGER,
  vehmodel INTEGER,
  vehlocation INTEGER,
  vehlicense VARCHAR,
  vehprice DECIMAL,
  vehstatus BOOLEAN
)
RETURNS BOOLEAN AS
$$
BEGIN
  UPDATE vehicle
  SET veh_model = vehmodel, veh_location = vehlocation, 
  veh_license = vehlicense, veh_price = vehprice, 
  veh_status = vehstatus
  WHERE veh_id = vehid;
  IF FOUND THEN
    RETURN TRUE;
  ELSE
    RETURN FALSE;
  END IF;
END;
$$
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION UpdateVehicleStatus(
  vehid INTEGER,
  vehstatus BOOLEAN
)
RETURNS BOOLEAN AS
$$
BEGIN
  UPDATE vehicle
  SET veh_status = vehstatus
  WHERE veh_id = vehid;
  IF FOUND THEN
    RETURN TRUE;
  ELSE
    RETURN FALSE;
  END IF;
END;
$$
LANGUAGE 'plpgsql';



CREATE OR REPLACE FUNCTION GetVehicleByBrand(brandId INTEGER) 
RETURNS TABLE (
  vehid INTEGER,
  vehmodel INTEGER,
  vehlocation INTEGER,
  vehlicense VARCHAR,
  vehprice DECIMAL,
  vehstatus BOOLEAN
) AS $$
BEGIN
  RETURN QUERY 
  SELECT veh_id, veh_model, veh_location, veh_license, veh_price, veh_status FROM vehicle, 
  veh_model AS Model, veh_brand AS Brand WHERE veh_model = Model.vm_id 
  AND Model.vm_brand = Brand.vb_id AND  Brand.vb_id = brandId;
END; $$ 
LANGUAGE 'plpgsql';


CREATE OR REPLACE FUNCTION GetVehicleByModel(ModelId INTEGER) 
RETURNS TABLE (
  vehid INTEGER,
  vehmodel INTEGER,
  vehlocation INTEGER,
  vehlicense VARCHAR,
  vehprice DECIMAL,
  vehstatus BOOLEAN
) AS $$
BEGIN
  RETURN QUERY 
  SELECT veh_id, veh_model, veh_location, veh_license, veh_price, veh_status FROM vehicle, 
  veh_model AS Model WHERE veh_model = Model.vm_id AND Model.vm_id = ModelId;
END; $$ 
LANGUAGE 'plpgsql';


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
CREATE OR REPLACE FUNCTION getAutoParameters(
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
CREATE OR REPLACE FUNCTION GetTravels(userId INTEGER) 
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
CREATE OR REPLACE FUNCTION GetLocationsByTravel(travelId INTEGER)
RETURNS TABLE (
	locationId INTEGER, 
	locationCity VARCHAR,
  locationCountry VARCHAR
) AS $$
BEGIN
RETURN QUERY
	SELECT TL.tl_loc_fk, L.loc_city, L.loc_country
	FROM TRA_LOC TL
	INNER JOIN public.LOCATION L ON TL.tl_loc_fk = L.loc_id
	WHERE TL.tl_tra_fk = travelId; 
END; $$
LANGUAGE plpgsql;  

--DROP FUNCTION AddTravel(VARCHAR, VARCHAR, VARCHAR, VARCHAR, BIGINT);
CREATE OR REPLACE FUNCTION AddTravel(
	travelName VARCHAR,  
	travelInit VARCHAR,
	travelEnd VARCHAR,
  travelDescription VARCHAR,
	userId INTEGER)
RETURNS BIGINT AS
$$
DECLARE
	travelId INTEGER;
BEGIN
	INSERT INTO Travel(tra_name, tra_ini, tra_end, tra_descr, tra_use_fk)
	VALUES(travelName, to_date(travelInit,'YYYY-MM-DD'), to_date(travelEnd,'YYYY-MM-DD'), travelDescription, userId) RETURNING tra_id INTO travelId;
	RETURN travelId;
END;
$$
LANGUAGE 'plpgsql';

-- DROP FUNCTION AddLocationToTravel(BIGINT, INTEGER);
CREATE OR REPLACE FUNCTION AddLocationToTravel(
	travelId INTEGER, 
	locationId INTEGER	
)
RETURNS BOOLEAN AS $$
BEGIN	
	INSERT INTO tra_loc (tl_tra_fk, tl_loc_fk)
	VALUES (travelId, locationId);
	IF FOUND THEN
		RETURN TRUE;
	ELSE
		RETURN FALSE;
	END IF;
END; $$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION GetReservationsOfHotelByTravelAndLocation(
	travelId INTEGER, 
	locationId INTEGER
) RETURNS TABLE (
	reservationId INTEGER,
	checkin TIMESTAMP,
	checkout TIMESTAMP,
	timest TIMESTAMP,
	userId INTEGER,
	hotelId INTEGER
)AS $$
BEGIN
	RETURN QUERY
	SELECT rh.rr_id, rh.rr_checkindate, rh.rr_checkoutdate, rh.rr_timestamp, rh.rr_use_fk, rh.rr_hot_fk 
	FROM TRA_RES tr
		  INNER JOIN 
		  TRA_LOC tl
			    ON tr.tr_travel_fk = tl.tl_tra_fk
		    INNER JOIN
		    RES_ROO rh
			    ON tr.tr_res_roo_fk = rh.rr_id
			    INNER JOIN HOTEL h
			        ON rh.rr_hot_fk = h.hot_id
		  WHERE tr.tr_travel_fk = travelId AND tl.tl_loc_fk = locationId AND h.hot_loc_fk = locationId;
END; $$
LANGUAGE plpgsql;

-- DROP FUNCTION updatetravel(integer,character varying,character varying,character varying,character varying);
CREATE OR REPLACE FUNCTION UpdateTravel(
	travelId INTEGER,
	travelName VARCHAR,
	travelDescription VARCHAR,
	travelInit VARCHAR,
	travelEnd VARCHAR
)
RETURNS BOOLEAN AS
$$
BEGIN
	UPDATE travel
	SET tra_name = travelName, tra_descr = travelDescription,
	tra_ini = to_date(travelInit,'YYYY-MM-DD'), tra_end = to_date(travelEnd,'YYYY-MM-DD')
	WHERE tra_id = travelId;
	IF FOUND THEN
		RETURN TRUE;
	ELSE
		RETURN FALSE;
	END IF;
END;
$$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION AddReservationToTravel(
	travelId INTEGER,
	reservationId INTEGER,
	reservationType VARCHAR
)
RETURNS BOOLEAN AS
$$
BEGIN 
	IF reservationType = 'HOTEL' THEN 
		INSERT INTO TRA_RES (TR_TRAVEL_FK, TR_RES_CRU_FK, TR_RES_FLI_FK, TR_RES_REST_FK, TR_RES_AUT_FK, TR_RES_ROO_FK, TR_TYPE)
		VALUES	(travelId, null, null, null, null, reservationId, reservationType);
	ELSIF reservationType = 'RESTAURANT' THEN
		INSERT INTO TRA_RES (TR_TRAVEL_FK, TR_RES_CRU_FK, TR_RES_FLI_FK, TR_RES_REST_FK, TR_RES_AUT_FK, TR_RES_ROO_FK, TR_TYPE)
		VALUES	(travelId, null, null, reservationId, null, null, reservationType);
	ELSIF reservationType = 'FLIGHT' THEN
		INSERT INTO TRA_RES (TR_TRAVEL_FK, TR_RES_CRU_FK, TR_RES_FLI_FK, TR_RES_REST_FK, TR_RES_AUT_FK, TR_RES_ROO_FK, TR_TYPE)
		VALUES	(travelId, null, reservationId, null, null, null, reservationType);
	ELSIF reservationType = 'CAR' THEN
		INSERT INTO TRA_RES (TR_TRAVEL_FK, TR_RES_CRU_FK, TR_RES_FLI_FK, TR_RES_REST_FK, TR_RES_AUT_FK, TR_RES_ROO_FK, TR_TYPE)
		VALUES	(travelId, null, reservationId, null, null, null, reservationType);
	END IF;
	IF FOUND THEN
		RETURN TRUE;
	ELSE
		RETURN FALSE;
	END IF;
	
END; $$
LANGUAGE plpgsql;



------------------------------------fin de grupo 10---------------------------------
------------------------------------Grupo12-----------------------------------------
------------------------------------------------------------------------------------
--Agregar Reserva de Vuelo
CREATE OR REPLACE FUNCTION AddReservationFlight(seatNum VARCHAR,tim VARCHAR,numPas INTEGER,
id_user INTEGER,id_fli INTEGER)
  RETURNS INTEGER AS $$
  DECLARE 
    ref_res_id INTEGER;
  
  BEGIN
    INSERT INTO res_fli(rf_seatnum,rf_timestamp,rf_num_ps,rf_use_fk,rf_fli_fk)
      VALUES(seatNum,TO_TIMESTAMP(tim, 'yyyy-mm-dd hh24:mi'),numPas,id_user,id_fli)RETURNING rf_id INTO ref_res_id;
    
    RETURN ref_res_id;
  END;
  $$ LANGUAGE plpgsql;

--Cancelar Reservar de Vuelo
CREATE OR  REPLACE FUNCTION deleteReservationFlight(id_reservation INTEGER) RETURNS INTEGER AS $$
  DECLARE res_id_fli INTEGER;
  BEGIN
    DELETE FROM RES_FLI WHERE rf_id = id_reservation RETURNING rf_id INTO res_id_fli;

    RETURN res_id_fli;
  END;
  $$ LANGUAGE plpgsql;

  --retornar Reserva de vuelo de un Pasajero
CREATE OR REPLACE FUNCTION getReservationFlight(usuario INTEGER) 
RETURNS TABLE (id INTEGER,price numeric,fecha_res_fli timestamp,seatnum character varying(100),country_s varchar,city_s varchar,id_arrival integer,numpas integer) AS $$
  BEGIN
    RETURN QUERY select r.rf_id as id,f.fli_price ,f.fli_departuredate as salida, r.rf_seatnum as asiento, l.loc_country as pais_salida, l.loc_city as ciudad_salida,f.fli_loc_arrival,r.rf_num_ps
    FROM res_fli r, flight f, plane p, location l, users u
   where f.fli_id = r.rf_fli_fk and r.rf_use_fk = usuario and u.use_id = r.rf_use_fk and p.pla_id = f.fli_pla_fk and l.loc_id = f.fli_loc_departure;  
  END;
  $$ LANGUAGE plpgsql;

  







---Devuelve la capacidad de un vuelo
  CREATE OR REPLACE FUNCTION getCapacity(idflight INTEGER)RETURNS integer AS $$  
DECLARE capacity Integer;
BEGIN
 SELECT p.pla_capacity FROM flight as f ,plane as p WHERE f.fli_id=idflight into capacity ;
 return capacity;
END;
$$ 
LANGUAGE plpgsql;
---Devuelve la cantidad de asientos reservados
CREATE OR REPLACE FUNCTION public.getsum(idflight integer)
  RETURNS integer AS
$BODY$  
DECLARE sum Integer;
BEGIN
 SELECT SUM(rf_num_ps) FROM res_fli as f WHERE f.rf_fli_fk=idflight into sum ;

	IF(sum>0) then
	  return sum;
	END IF; 	
	
 return 0;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public.getsum(integer)
  OWNER TO vacanza;

--Devolver id ciudad
CREATE OR REPLACE FUNCTION GetIDLocation(name_city VARCHAR) 
RETURNS TABLE (id INTEGER) AS $$
  BEGIN
     RETURN QUERY SELECT loc.loc_id
     FROM location as loc 
     where loc.loc_city=name_city; 
  END;   
  $$ LANGUAGE plpgsql;

 --Devuelve Lista validada de una reserva de vuelo (IDA)
 CREATE OR REPLACE FUNCTION GetFlightsIDA(_departure integer,_arrival integer,_departuredate char varying)
    RETURNS TABLE(id integer, price numeric, departuredate timestamp without time zone, arrivaldate timestamp without time zone, locdeparture integer, locarrival integer) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
  fli_id,fli_price, fli_departuredate, fli_arrivaldate, fli_loc_departure, fli_loc_arrival
	FROM public.Flight WHERE fli_departuredate::date = _departuredate::timestamp without time zone  and fli_loc_departure = _departure and fli_loc_arrival = _arrival;
END;

$BODY$;

ALTER FUNCTION public.GetFlightsIDA(integer, integer, char varying)
    OWNER TO vacanza;  

-----Devuelve los datos de la tabla location
CREATE OR REPLACE FUNCTION GetNameLocation(_id_city integer)
    RETURNS TABLE(id integer, namecity varchar,namecountry varchar) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

BEGIN
	RETURN QUERY SELECT
		loc_id,loc_city,loc_country
	FROM location WHERE location.loc_id=_id_city;
END;

$BODY$;

ALTER FUNCTION public.GetNameLocation(integer)
    OWNER TO vacanza; 

--Devuelve Lista validada de una reserva de vuelo (IDA y Vuelta)
CREATE OR REPLACE FUNCTION public.GetFlightsIDAVU(_departure integer,_arrival integer,_departuredate char varying,_arrivaldate char varying)
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

ALTER FUNCTION public.GetFlightsIDAVU(integer, integer, char varying, char varying)
    OWNER TO vacanza; 

------------------------------------Fin Grupo12--------------------------------------------------
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
CREATE OR REPLACE FUNCTION getResRestaurant(usuario INTEGER) RETURNS TABLE ( id INTEGER,
  ciudad VARCHAR, pais VARCHAR, restaurant VARCHAR, direccion VARCHAR,
  fecha_reservacion TIMESTAMP, cantPeople INTEGER) AS $$
  
  BEGIN
    RETURN QUERY select m.rr_id, l.loc_city, l.loc_country, r.res_name, r.res_address_specs, m.rr_date, m.rr_num_ppl
    FROM restaurant r, res_rest m, location l, users u
    where m.rr_use_fk = usuario and r.res_id = m.rr_res_fk and l.loc_id = r.res_loc_fk and U.USE_ID = m.rr_use_fk;

  END;
  $$ LANGUAGE plpgsql;

--SP para retornar aquellas reservas que no han sido pagadas
CREATE OR REPLACE FUNCTION getReservationNotPay(usuario INTEGER) RETURNS TABLE 
  (id INTEGER, f_reserva TIMESTAMP) AS $$

  BEGIN
    RETURN QUERY select r.rr_id, r.rr_date
    FROM res_rest r, users u
    where r.rr_use_fk = usuario and u.use_id = rr_use_fk and rr_pay_fk isNull;
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

-----------------------------------fin grupo 14-----------------------------------------------------------
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
          select USERS.use_name,USERS.use_last_name,USERS.use_password from USERS WHERE USERS.use_email=$1;
        END
$BODY$ LANGUAGE plpgsql;
---------------------------------finGrupo1---------------------------------------------------------------------------


----------------------------------Grupo 13 Automobile Reservations-----------------------------------------

--Get all Reservations Automobiles
CREATE OR REPLACE FUNCTION public.m13_getresautos(
    )
    RETURNS TABLE(ra_id integer, ra_pickupdate timestamp without time zone, ra_returndate timestamp without time zone, ra_use_fk integer, ra_aut_fk integer, ra_pay_fk integer, aut_id integer, aut_make character varying, aut_model character varying, aut_capacity integer, aut_price numeric, aut_license character varying, aut_picture character varying, aut_loc_fk integer)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$
BEGIN
 RETURN QUERY
  select rr.ra_id,rr.ra_pickupdate, rr.ra_returndate, rr.ra_use_fk,rr.ra_aut_fk,rr.ra_pay_fk,
  r.aut_id,r.aut_make,r.aut_model, r.aut_capacity,r.aut_price,r.aut_license,r.aut_picture,r.aut_loc_fk
  from public.res_aut as rr,public.Automobile as r
  where rr.ra_aut_fk = r.aut_id;
END;
$BODY$;

ALTER FUNCTION public.m13_getresautos()
    OWNER TO vacanza;

--Find by id de la reservation
CREATE OR REPLACE FUNCTION public.m13_findbyresautid(
    _resautid integer)
    RETURNS TABLE(ra_id integer, ra_pickupdate timestamp without time zone, ra_returndate timestamp without time zone, ra_timestamp timestamp without time zone, ra_use_fk integer, ra_aut_fk integer, ra_pay_fk integer)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$
BEGIN
  RETURN QUERY
    select ra.ra_id, ra.ra_pickupdate, ra.ra_returndate,ra.ra_timestamp,ra.ra_use_fk, ra.ra_aut_fk, ra.ra_pay_fk
    from public.res_aut as ra
         where _resautid = ra.ra_id;
END;
$BODY$;

ALTER FUNCTION public.m13_findbyresautid(integer)
    OWNER TO vacanza;

--Add Automobile Reservation
CREATE OR REPLACE FUNCTION public.m13_addautomobilereservation(
    _checkin timestamp without time zone,
    _checkout timestamp without time zone,
    _use_fk integer,
    _ra_aut_fk integer)
    RETURNS void
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$

BEGIN
INSERT INTO Res_Aut
(ra_pickupdate,ra_returndate,ra_timestamp,ra_use_fk,ra_aut_fk)
VALUES(_checkin,_checkout,CURRENT_TIMESTAMP,_use_fk,_ra_aut_fk);
END;

$BODY$;

ALTER FUNCTION public.m13_addautomobilereservation(timestamp without time zone, timestamp without time zone, integer, integer)
    OWNER TO vacanza;

--Update de una Automobile Reservation
CREATE OR REPLACE FUNCTION public.m13_updateautomobilereservation(
    _checkin timestamp without time zone,
    _checkout timestamp without time zone,
    _use_fk integer,
    _ra_aut_fk integer,
    _ra_id integer)
    RETURNS void
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$

BEGIN
UPDATE Res_Aut SET
ra_pickupdate = _checkin,
ra_returndate = _checkout,
ra_timestamp = CURRENT_TIMESTAMP,
ra_use_fk =_use_fk,
ra_aut_fk = _ra_aut_fk
WHERE _ra_id = ra_id;
END;

$BODY$;

ALTER FUNCTION public.m13_updateautomobilereservation(timestamp without time zone, timestamp without time zone, integer, integer, integer)
    OWNER TO vacanza;

--Delete
CREATE OR REPLACE FUNCTION public.m13_deleteautomobilereservation(
    _rar integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$

DECLARE
BEGIN
    EXECUTE format('DELETE from public.res_aut WHERE ra_id= %L', _rar);
    return _rar;
END;

$BODY$;

ALTER FUNCTION public.m13_deleteautomobilereservation(integer)
    OWNER TO vacanza;



--Get Reservation By User ID
CREATE OR REPLACE FUNCTION public.m13_getresautomobilebyuserid(
    _user_id integer)
    RETURNS TABLE(ra_id integer, ra_pickupdate timestamp without time zone, ra_returndate timestamp without time zone, ra_timestamp timestamp without time zone, ra_aut_fk integer, ra_use_fk integer, ra_pay_fk integer, aut_id integer, aut_make character varying, aut_model character varying, aut_capacity integer, aut_price numeric)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$

BEGIN
 RETURN QUERY
  select ra.ra_id, ra.ra_pickupdate, ra.ra_returndate,ra.ra_timestamp,
  ra.ra_aut_fk,ra.ra_use_fk,ra.ra_pay_fk,au.aut_id, au.aut_make, au.aut_model,
  au.aut_capacity,au.aut_price
  from public.Res_Aut as ra, public.Automobile as au
  where ra.ra_aut_fk= au.aut_id and _user_id = ra.ra_use_fk ;
END;

$BODY$;

ALTER FUNCTION public.m13_getresautomobilebyuserid(integer)
    OWNER TO vacanza;

----------------------------------FIN Grupo 13 Automobile Reservations------------------------------------

----------------------------------Grupo 13 Room Reservations------------------------------------------------

CREATE OR REPLACE FUNCTION public.m13_getresrooms(
    )
    RETURNS TABLE(rr_id integer, rr_checkindate timestamp without time zone, rr_checkoutdate timestamp without time zone, rr_timestamp timestamp without time zone, rr_hot_fk integer, rr_use_fk integer, rr_pay_fk integer, hot_id integer)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$
BEGIN
 RETURN QUERY
  select rr.rr_id, rr.rr_checkinDate, rr.rr_checkoutDate,rr.rr_timestamp, rr.rr_hot_fk,rr.rr_use_fk,rr.rr_pay_fk,
  h.hot_id
  from public.Res_Roo as rr, public.Hotel as h
  where rr.rr_hot_fk= h.hot_id;
END;
$BODY$;

ALTER FUNCTION public.m13_getresrooms()
    OWNER TO vacanza;

CREATE OR REPLACE FUNCTION public.m13_findbyroomreservationid(
    _resrooid integer)
    RETURNS TABLE(res_roo_id integer, res_roo_fecha_ingreso timestamp without time zone, res_roo_fecha_salida timestamp without time zone, rr_timestamp timestamp without time zone, rr_hot_fk integer, rr_use_fk integer, rr_pay_fk integer)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$
BEGIN
  RETURN QUERY
    select rr.rr_id, rr.rr_checkindate, rr.rr_checkoutdate,rr.rr_timestamp, rr.rr_hot_fk,rr.rr_use_fk,rr.rr_pay_fk
    from public.res_roo as rr
         where _resrooid = rr.rr_id;
END;
$BODY$;

ALTER FUNCTION public.m13_findbyroomreservationid(integer)
    OWNER TO vacanza;

--ADD room reservation
CREATE OR REPLACE FUNCTION public.m13_addroomreservation(
    _checkin timestamp without time zone,
    _checkout timestamp without time zone,
    _use_fk integer,
    _rr_hot_fk integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$
DECLARE
_id INTEGER;
BEGIN
INSERT INTO Res_Roo
(rr_checkindate,rr_checkoutdate,rr_timestamp,rr_use_fk,rr_hot_fk)
VALUES(_Checkin,_Checkout,CURRENT_TIMESTAMP,_use_fk,_rr_hot_fk)
RETURNING rr_id into _id;
return _id;
END;
$BODY$;

ALTER FUNCTION public.m13_addroomreservation(timestamp without time zone, timestamp without time zone, integer, integer)
    OWNER TO vacanza;


--UPDATE Room Reservation
CREATE OR REPLACE FUNCTION public.m13_updatehotelreservation(
    _checkin timestamp without time zone,
    _checkout timestamp without time zone,
    _use_fk integer,
    _rr_hot_fk integer,
    _rr_id integer)
    RETURNS void
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$
BEGIN
UPDATE Res_roo SET
rr_checkindate = _checkin,
rr_checkoutdate = _checkout,
rr_timestamp = CURRENT_TIMESTAMP,
rr_use_fk =_use_fk,
rr_hot_fk = _rr_hot_fk
WHERE _rr_id = rr_id;
END;

$BODY$;

ALTER FUNCTION public.m13_updatehotelreservation(timestamp without time zone, timestamp without time zone, integer, integer, integer)
    OWNER TO vacanza;



--DELETE Room Reservation
CREATE OR REPLACE FUNCTION public.m13_deleteroomreservation(
    _rooid integer)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
AS $BODY$
DECLARE
BEGIN
    EXECUTE format('DELETE from public.res_roo WHERE rr_id= %L', _rooid);
    return _rooid;
END;
$BODY$;

ALTER FUNCTION public.m13_deleteroomreservation(integer)
    OWNER TO vacanza;

CREATE OR REPLACE FUNCTION public.m13_getresroobyuserandroomid(
    _user_id integer)
    RETURNS TABLE(rr_id integer, rr_checkindate timestamp without time zone, rr_checkoutdate timestamp without time zone, rr_hot_fk integer, hot_name character varying, hot_room_capacity integer, hot_room_price numeric, hot_phone character varying, hot_stars integer)
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE
    ROWS 1000
AS $BODY$

BEGIN
 RETURN QUERY
  select rr.rr_id,
  rr.rr_checkindate,
  rr.rr_checkoutdate,
  rr.rr_hot_fk,
  h.hot_name,
  h.hot_room_capacity,
  h.hot_room_price,
  h.hot_phone,
  h.hot_stars
  from public.Res_Roo as rr, public.Hotel as h
  where rr.rr_hot_fk= h.hot_id and _user_id = rr.rr_use_fk ;
END;

$BODY$;

ALTER FUNCTION public.m13_getresroobyuserandroomid(integer)
    OWNER TO vacanza;
--AVAILABILITY HOTEL
CREATE OR REPLACE FUNCTION getAvailableRoomsBasedOnReservationByHotelId(
    IN _hot_id integer, OUT disponibles int)
AS $$
BEGIN
disponibles := (SELECT ((Select hot_room_qty FROM hotel where hot_id = _hot_id)
    - (SELECT COUNT(rr_hot_fk) FROM res_roo where rr_hot_fk=_hot_id  and rr_checkoutdate>=CURRENT_TIMESTAMP)
) AS "Habitaciones Disponibles");

END;

$$ LANGUAGE plpgsql;

-------SP para PAYMENT EN las RESERVAS---------------
CREATE OR REPLACE FUNCTION m13_modifyReservationRoomPayment(pay Integer,reservation Integer) RETURNS
INTEGER AS $$
DECLARE res_id INTEGER;
BEGIN
UPDATE res_roo set rr_pay_fk = pay
where(rr_id = reservation) returning rr_id INTO res_id;
return res_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION m13_modifyReservationAutomobilePayment(pay Integer,reservation Integer) RETURNS
INTEGER AS $$
DECLARE res_id INTEGER;
BEGIN
UPDATE res_aut set ra_pay_fk = pay
where(ra_id = reservation) returning ra_id INTO res_id;
return res_id;
END;
$$ LANGUAGE plpgsql;


----------------------------------FIN Grupo 13 Room Reservations-------------------------------------------

----------------------------------- grupo 11-----------------------------------------------------------


--SP QUE AÑADE PAGO REGISTRADO
CREATE OR REPLACE FUNCTION addPayment(_paymetMethod varchar(50), _pay_total Double Precision)

RETURNS integer

AS $$
DECLARE id integer;
BEGIN
INSERT INTO public.payment(
            pay_method,
            pay_total, 
            pay_timestamp)
 VALUES    (_paymetMethod, 
            _pay_total, 
            CURRENT_TIMESTAMP )
            RETURNING pay_id INTO id;
            RETURN id;
END       
$$  LANGUAGE plpgsql;


--SP QUE TRAE LISTA DE ORDENES
-- Function: public.getinfoorder(bigint, integer)

-- DROP FUNCTION public.getinfoorder(bigint, integer);

CREATE OR REPLACE FUNCTION getinfoorder(
    IN _id bigint,
    IN _tipo integer)
  RETURNS TABLE(id integer,
   descrip character varying,
    image character varying,
	brand character varying,
	qty double precision,
	price double precision,
	price_total double precision) AS
$BODY$
BEGIN
	IF _tipo = 0 
	THEN

	 RETURN QUERY
		SELECT 
		RA_ID AS ID,
		AUT_PICTURE AS IMAGE,
		AUT_MAKE AS DESCRIP,
		AUT_MODEL AS BRAND,
		DATE_PART('day',RA_RETURNDATE - RA_PICKUPDATE) AS QTY,
		cast(AUT_PRICE as double precision) AS PRICE,
		AUT_PRICE * DATE_PART('day',RA_RETURNDATE - RA_PICKUPDATE) AS PRICE_TOTAL
		FROM RES_AUT 
		INNER JOIN AUTOMOBILE ON RA_AUT_FK = AUT_ID
		WHERE RA_ID = _id;
		
	ELSIF _tipo = 1 
	THEN
	
	 RETURN QUERY
		SELECT 
		RC_ID AS ID,
		SHI_PICTURE AS IMAGE,
		SHI_NAME AS DESCRIP,
		SHI_MODEL AS BRAND,
		DATE_PART('day',CRU_ARRIVALDATE - CRU_DEPARTUREDATE) AS QTY,
		cast(CRU_PRICE as double precision) AS PRICE,
		CRU_PRICE * DATE_PART('day',CRU_ARRIVALDATE - CRU_DEPARTUREDATE) AS PRICE_TOTAL
		FROM RES_CRU 
		INNER JOIN CRUISE ON CRU_ID = RC_CRU_FK
		INNER JOIN SHIP ON CRU_SHI_FK = CRU_ID
		WHERE RC_ID = _id;

		ELSIF _tipo = 2
	THEN
	
	 RETURN QUERY
		SELECT 
		RA_ID AS ID,
		AUT_PICTURE AS IMAGE,
		AUT_MAKE AS DESCRIP,
		AUT_MODEL AS BRAND,
		DATE_PART('day',RA_RETURNDATE - RA_PICKUPDATE) AS QTY,
		cast(AUT_PRICE as double precision) AS PRICE,
		AUT_PRICE * DATE_PART('day',RA_RETURNDATE - RA_PICKUPDATE) AS PRICE_TOTAL
		FROM RES_AUT 
		INNER JOIN AUTOMOBILE ON RA_AUT_FK = AUT_ID
		WHERE RA_ID = _id;

		ELSIF _tipo = 3
	THEN
	
	 RETURN QUERY
		SELECT 
		RR_ID 		 AS ID,
		RES_PICTURE 	 AS IMAGE,
		RES_NAME    	 AS DESCRIP,
		RES_BUSINESSNAME AS BRAND,
		cast('1' as double precision)  		 AS QTY,
		cast(RES_PRICE as double precision) AS PRICE,
		cast(RES_PRICE as double precision) AS PRICE_TOTAL
		FROM RES_REST 
		INNER JOIN RESTAURANT ON RR_RES_FK = RES_ID
		WHERE RR_ID = _id;

	END IF;
END
$BODY$
    LANGUAGE plpgsql; 
  -- select * from getinfoorder(3,3)

  --SP QUE TRAE LISTA DE ORDENES VERSION 2 MEJORADA

-- DROP FUNCTION public.getinfoorderAll(bigint, bigint,bigint,bigint);

CREATE OR REPLACE FUNCTION getinfoorderAll(
    IN _idAuto bigint,
    IN _idRoo bigint,
    IN _idRes bigint,
    IN _idCru bigint)
  RETURNS TABLE(id integer,
   descrip character varying,
    image character varying,
	brand character varying,
	qty double precision,
	price double precision,
	price_total double precision) AS
$BODY$
BEGIN

	 RETURN QUERY
		SELECT 
		RA_ID AS ID,
		CAST('Automobil: ' || AUT_MAKE AS character varying ) AS DESCRIP,
		AUT_PICTURE AS IMAGE,
		AUT_MODEL AS BRAND,
		DATE_PART('day',RA_RETURNDATE - RA_PICKUPDATE) AS QTY,
		cast(AUT_PRICE as double precision) AS PRICE,
		AUT_PRICE * DATE_PART('day',RA_RETURNDATE - RA_PICKUPDATE) AS PRICE_TOTAL
		FROM RES_AUT 
		INNER JOIN AUTOMOBILE ON RA_AUT_FK = AUT_ID
		WHERE RA_ID = _idAuto
		
		UNION ALL	
		SELECT 
		RR_ID AS ID,
		CAST('Habitacion: ' || hot_name AS character varying ) AS DESCRIP,
		hot_picture AS IMAGE,
		hot_website AS BRAND,
		DATE_PART('day',RR_CHECKOUTDATE - RR_CHECKINDATE) AS QTY,
		cast(hot_room_price as double precision) AS PRICE,
		hot_room_price * DATE_PART('day',RR_CHECKOUTDATE - RR_CHECKINDATE) AS PRICE_TOTAL
		FROM RES_ROO
		INNER JOIN hotel ON rr_hot_fk = hot_id
		WHERE RR_ID = _idRoo
		
		UNION ALL
		SELECT 
		RR_ID 		 AS ID,
		CAST('Resaturante: ' || RES_NAME AS character varying ) AS DESCRIP,
		RES_PICTURE 	 AS IMAGE,
		RES_BUSINESSNAME AS BRAND,
		cast('1' as double precision)  		 AS QTY,
		cast(RES_PRICE as double precision) AS PRICE,
		cast(RES_PRICE as double precision) AS PRICE_TOTAL
		FROM RES_REST 
		INNER JOIN RESTAURANT ON RR_RES_FK = RES_ID
		WHERE RR_ID = _idRes

		UNION ALL
		SELECT 
		RC_ID AS ID,
		CAST('Crucero ' || SHI_NAME AS character varying ) AS DESCRIP,
		SHI_PICTURE AS IMAGE,
		SHI_MODEL AS BRAND,
		DATE_PART('day',CRU_ARRIVALDATE - CRU_DEPARTUREDATE) AS QTY,
		cast(CRU_PRICE as double precision) AS PRICE,
		CRU_PRICE * DATE_PART('day',CRU_ARRIVALDATE - CRU_DEPARTUREDATE) AS PRICE_TOTAL
		FROM RES_CRU 
		INNER JOIN CRUISE ON CRU_ID = RC_CRU_FK
		INNER JOIN SHIP ON CRU_SHI_FK = CRU_ID
		WHERE RC_ID = _idCru;

END
$BODY$
    LANGUAGE plpgsql; 
  -- select * from getinfoorderAll(3,3,3,3)

  --SP QUE TRAE RESERVAS NO PAGADAS AUTOS Y HABITACION
CREATE OR REPLACE FUNCTION getNoPaysResAutHab(
    IN _id bigint,
    IN _tipo integer)
  RETURNS TABLE(
    id integer,
    name character varying,
    dateR timestamp) 
    AS
$BODY$
BEGIN
	IF _tipo = 0 
		THEN
	     RETURN QUERY
		SELECT 
		RA_ID AS ID,
		aut_make AS NAME,
		RA_TIMESTAMP AS DATER
		FROM RES_AUT
		INNER JOIN AUTOMOBILE ON RA_AUT_FK = AUT_ID
		WHERE RA_USE_FK = _ID
		AND RA_PAY_FK IS  NULL;
		
	    ELSEIF _tipo = 1 
		THEN
	      RETURN QUERY
	      SELECT 
	      RR_ID AS ID,
	      HOT_NAME AS NAME,
	      RR_TIMESTAMP AS DATER
	      from RES_ROO
	      INNER JOIN HOTEL ON RR_HOT_FK = HOT_ID
	      WHERE RR_USE_FK =_ID
	      AND RR_PAY_FK IS NULL;
     ELSEIF _tipo = 2 
		THEN
	      RETURN QUERY
	      SELECT 
	      RR_ID AS ID,
	      RES_NAME AS NAME,
	      RR_TIMESTAMP AS DATER
	      from RES_REST
	      INNER JOIN RESTAURANT ON RR_ID = res_id
	      WHERE RR_USE_FK =_ID
	      AND RR_PAY_FK IS NULL;
	      
	END IF;
END
$BODY$
      LANGUAGE plpgsql; 

 -- SELECT * FROM getNoPaysResAutHab(1,0)

  --DROP FUNCTION getnopaysresauthab(bigint,integer)


  --SP QUE Actualiza id de pago en tablas afectadas
  CREATE OR REPLACE FUNCTION PutPayProccess(
    IN _idPay bigint,
    IN _idAuto bigint,
    IN _idRoo bigint,
    IN _idRes bigint,
    IN _idCru bigint)
RETURNS BIGINT
    AS
$BODY$
BEGIN

UPDATE RES_AUT
SET RA_PAY_FK = _idPay
WHERE RA_ID = _idAuto;

UPDATE RES_ROO
SET RR_PAY_FK = _idPay
WHERE RR_ID = _idRoo;

UPDATE RES_REST
SET RR_PAY_FK = _idPay
WHERE RR_ID = _idRes;

SELECT _idRes;
END
$BODY$
      LANGUAGE plpgsql; 


--Consulta Pagos Realizados
CREATE OR REPLACE FUNCTION getMyPayments(
    IN _id bigint)
  RETURNS TABLE(
    id integer,
    descrip character varying,
    pm character varying,
    amount numeric,
    dater timestamp) 
    AS
$BODY$
BEGIN
RETURN QUERY
   SELECT 
   PAY_ID AS TRANSACT,
   CAST('Habitacion' AS character varying ) AS DESCRIP,
   PAY_METHOD AS PM,
   PAY_TOTAL  AS AMOUNT,
   PAY_TIMESTAMP AS DATER
   FROM PAYMENT
   INNER JOIN RES_ROO ON RR_PAY_FK = PAY_ID
   WHERE RR_USE_FK = _ID
   UNION
      SELECT 
   PAY_ID AS TRANSACT,
   CAST('Auto' AS character varying ) AS DESCRIP,
   PAY_METHOD AS PM,
   PAY_TOTAL  AS AMOUNT,
   PAY_TIMESTAMP AS DATER
   FROM PAYMENT
   INNER JOIN RES_AUT ON RA_PAY_FK = PAY_ID
   WHERE RA_USE_FK = _ID
   UNION
    SELECT 
   PAY_ID AS TRANSACT,
   CAST('Restaurant' AS character varying ) AS DESCRIP,
   PAY_METHOD AS PM,
   PAY_TOTAL  AS AMOUNT,
   PAY_TIMESTAMP AS DATER
   FROM PAYMENT
   INNER JOIN RES_REST ON RR_PAY_FK = PAY_ID
   WHERE RR_USE_FK = _ID;
END
$BODY$
    LANGUAGE plpgsql;


-----------------------------------fin grupo 11-----------------------------------------------------------

