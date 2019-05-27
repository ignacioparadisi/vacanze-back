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

CREATE SEQUENCE SEQ_Claim
  START WITH 1
  INCREMENT BY 1
  NO MINVALUE
  NO MAXVALUE
  CACHE 1;
  
CREATE TABLE Claim(
    cla_id integer  DEFAULT nextval('SEQ_Claim'),
    cla_title varchar(30) NOT NULL,
    cla_descr varchar(30) NOT NULL,
    cla_status varchar(20) CHECK (cla_status ='ABIERTO' OR cla_status='CERRADO' OR cla_status='ESPERA'), 
    CONSTRAINT pk_Claim PRIMARY KEY(cla_id)
    --CONSTRAINT pf_equipaje FOREIGN KEY (cla_equi_id) REFERENCES JUGADOR(equi_id) ON DELETE CASCADE ON UPDATE CASCADE, 
);


CREATE TABLE Lugar (
  l_id SERIAL,
  l_tipo CHAR(1) NOT NULL,
  l_nombre VARCHAR(100) NOT NULL,
  fk_lugar INTEGER,
  CONSTRAINT pk_lugar PRIMARY KEY (l_id),
  CONSTRAINT check_tipo CHECK(l_tipo in ('P','C')) ---- P de pais y C de ciudad ------
);

CREATE TABLE Hotel (
                       hot_id                SERIAL,
                       hot_nombre            VARCHAR(100) NOT NULL,
                       hot_cant_habitaciones INTEGER      NOT NULL,
                       hot_activo            BOOLEAN      NOT NULL DEFAULT TRUE,
                       hot_telefono          VARCHAR(20)  NOT NULL,
                       hot_sitio_web         VARCHAR(100),
                       hot_fk_lugar          INTEGER      NOT NULL,
                       CONSTRAINT pk_hotel PRIMARY KEY (hot_id),
                       CONSTRAINT fk_hotel_lugar FOREIGN KEY (hot_fk_lugar) REFERENCES Lugar (l_id)
);

CREATE SEQUENCE SEQ_AUTOMOBILE
  START WITH 1
  INCREMENT BY 1
  NO MINVALUE
  NO MAXVALUE
  CACHE 1;
  
CREATE TABLE AUTOMOBILE(
  AUT_ID INTEGER NOT NULL,
  AUT_MAKE VARCHAR(30) NOT NULL,
  AUT_MODEL VARCHAR(30) NOT NULL,
  AUT_CAPACITY INTEGER NOT NULL,
  AUT_ISACTIVE BOOLEAN DEFAULT TRUE NOT NULL,
  AUT_PRICE DECIMAL NOT NULL,
  AUT_LICENSE VARCHAR(10) NOT NULL,
  AUT_PICTURE VARCHAR,
  AUT_LOC_FK INTEGER NOT NULL,
  CONSTRAINT PRIMARY_AUTOMOBILE PRIMARY KEY (AUT_ID),
  CONSTRAINT FOREIGN_AUT_LOCATION FOREIGN KEY (AUT_LOC_FK) REFERENCES LOCATION(LOC_ID) ON DELETE CASCADE ON UPDATE CASCADE
);

-------------------------------------------------------------------------------------------
