CREATE TABLE Lugar (
                     l_id SERIAL,
                     l_tipo CHAR(1) NOT NULL,
                     l_nombre VARCHAR(100) NOT NULL,
                     fk_lugar INTEGER,
                     CONSTRAINT pk_lugar PRIMARY KEY (l_id),
                     CONSTRAINT check_tipo CHECK(l_tipo in ('P','C')) ---- P de pais y C de ciudad ------
);

----------------------------------grupo 2-------------------------------------------
CREATE TABLE Role (
                    rol_id               SERIAL,
                    rol_name             VARCHAR(20) NOT NULL,
                    CONSTRAINT pk_role PRIMARY KEY (rol_id)
                  
);

CREATE TABLE Users (
                     use_id               SERIAL,
                     use_document_id      VARCHAR(50) NOT NULL,
                     use_email            VARCHAR(50) NOT NULL,
                     use_last_name        VARCHAR(50) NOT NULL,
                     use_name             VARCHAR(50) NOT NULL,
                     use_password         VARCHAR(50) NOT NULL,
                     CONSTRAINT pk_users PRIMARY KEY (use_id)
);

CREATE TABLE User_Role (
                         usr_id          SERIAL,
                         usr_rol_id     INTEGER,
                         usr_use_id     INTEGER,
                         CONSTRAINT pk_user_role PRIMARY KEY (usr_id, usr_rol_id, usr_use_id),
                         CONSTRAINT fk_role FOREIGN KEY (usr_rol_id) REFERENCES Role(rol_id),
                         CONSTRAINT fk_user FOREIGN KEY (usr_use_id) REFERENCES Users(use_id)
);


----------------------------------grupo 6-------------------------------------------

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
