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

CREATE OR REPLACE FUNCTION GetRolesForUser(user_id integer)
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
