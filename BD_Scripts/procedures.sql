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


   INSERT INTO AUTOMOBILE(AUT_ID,AUT_MAKE,AUT_MODEL,AUT_CAPACITY,AUT_ISACTIVE,AUT_LICENSE,AUT_PRICE,AUT_PICTURE,AUT_LOC_FK) VALUES
    (nextval('SEQ_AUTOMOBILE'), _make, _model,_capacity,_status,_licence,_price,_picture,_place);
   RETURN currval('SEQ_AUTOMOBILE');
END;
$$ LANGUAGE plpgsql;

-----------------------------------------------------------------------------------------
----------------eliminar auto-------------------------------

CREATE OR REPLACE FUNCTION DeleteAuto(_id integer)
RETURNS void AS
$$
BEGIN

    DELETE FROM AUTOMOBILE
    WHERE (AUT_ID = _id);

END;
$$ LANGUAGE plpgsql;
------------------------------------------------
CREATE OR REPLACE FUNCTION ConsultforIdAuto(codigo integer)
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
    FROM AUTOMOBILE  WHERE aut_id = codigo;
END;
$$ LANGUAGE plpgsql;

--------------------------------------------------------------------------------------------
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