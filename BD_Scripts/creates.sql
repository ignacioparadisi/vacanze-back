-------------------------------Grupo 3---------------------------------------------
-- SEQUENCE: public.fli_id_seq

-- DROP SEQUENCE public.fli_id_seq;

CREATE SEQUENCE public.fli_id_seq;

ALTER SEQUENCE public.fli_id_seq
    OWNER TO vacanza;

-- SEQUENCE: public.loc_id_seq

-- DROP SEQUENCE public.loc_id_seq;

CREATE SEQUENCE public.loc_id_seq;

ALTER SEQUENCE public.loc_id_seq
    OWNER TO vacanza;

-- SEQUENCE: public.pla_id_seq

-- DROP SEQUENCE public.pla_id_seq;

CREATE SEQUENCE public.pla_id_seq;

ALTER SEQUENCE public.pla_id_seq
    OWNER TO vacanza;

-- SEQUENCE: public.sto_id_seq

-- DROP SEQUENCE public.sto_id_seq;

CREATE SEQUENCE public.sto_id_seq;

ALTER SEQUENCE public.sto_id_seq
    OWNER TO vacanza;

-- Table: public.location

-- DROP TABLE public.location;

CREATE TABLE public.location
(
    loc_id integer NOT NULL DEFAULT nextval('loc_id_seq'::regclass),
    loc_city character varying(180) COLLATE pg_catalog."default" NOT NULL,
    loc_country character varying(180) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Location_pkey" PRIMARY KEY (loc_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.location
    OWNER to vacanza;

-- Table: public.plane

-- DROP TABLE public.plane;

CREATE TABLE public.plane
(
    pla_id integer NOT NULL DEFAULT nextval('pla_id_seq'::regclass),
    pla_autonomy double precision NOT NULL,
    "pla_isActive" boolean NOT NULL,
    pla_capacity integer NOT NULL,
    "pla_loadingCap" double precision NOT NULL,
    pla_model character varying(180) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT plane_pkey PRIMARY KEY (pla_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.plane
    OWNER to vacanza;

-- Table: public.flight

-- DROP TABLE public.flight;

CREATE TABLE public.flight
(
    fli_pla_fk integer NOT NULL,
    fli_price double precision NOT NULL,
    fli_departuredate date NOT NULL,
    fli_arrivaldate date NOT NULL,
    fli_id integer NOT NULL DEFAULT nextval('fli_id_seq'::regclass),
    CONSTRAINT "Flight_pkey" PRIMARY KEY (fli_id),
    CONSTRAINT fk_plane FOREIGN KEY (fli_pla_fk)
        REFERENCES public.plane (pla_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.flight
    OWNER to vacanza;

-- Table: public.stop

-- DROP TABLE public.stop;

CREATE TABLE public.stop
(
    sto_id integer NOT NULL DEFAULT nextval('sto_id_seq'::regclass),
    sto_fli_fk integer,
    sto_departuredate date NOT NULL,
    sto_arrivaldate date NOT NULL,
    sto_locdeparture integer NOT NULL,
    sto_locarrival integer NOT NULL,
    sto_cru_fk integer,
    CONSTRAINT "Stop_pkey" PRIMARY KEY (sto_id),
    CONSTRAINT fk_fli FOREIGN KEY (sto_fli_fk)
        REFERENCES public.flight (fli_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT loc_arrival_fk FOREIGN KEY (sto_locarrival)
        REFERENCES public.location (loc_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT loc_departure_fk FOREIGN KEY (sto_locdeparture)
        REFERENCES public.location (loc_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.stop
    OWNER to vacanza;

-- Index: fki_fk_fli

-- DROP INDEX public.fki_fk_fli;

CREATE INDEX fki_fk_fli
    ON public.stop USING btree
    (sto_fli_fk)
    TABLESPACE pg_default;

-- Index: fki_loc_arrival_fk

-- DROP INDEX public.fki_loc_arrival_fk;

CREATE INDEX fki_loc_arrival_fk
    ON public.stop USING btree
    (sto_locarrival)
    TABLESPACE pg_default;

-- Index: fki_loc_departure_fk

-- DROP INDEX public.fki_loc_departure_fk;

CREATE INDEX fki_loc_departure_fk
    ON public.stop USING btree
    (sto_locdeparture)
    TABLESPACE pg_default;

-- Index: fki_fk_plane

-- DROP INDEX public.fki_fk_plane;

CREATE INDEX fki_fk_plane
    ON public.flight USING btree
    (fli_pla_fk)
    TABLESPACE pg_default;

