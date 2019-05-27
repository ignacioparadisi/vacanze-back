INSERT INTO lugar (l_tipo, l_nombre, fk_lugar)
VALUES ('P', 'Lugar de prueba', null);

------- grupo 2 ----------

INSERT INTO Role (rol_name)
VALUES ('Cliente');

INSERT INTO Role (rol_name)
VALUES ('Administrador');

INSERT INTO Role (rol_name)
VALUES ('Checkin');

INSERT INTO Role (rol_name)
VALUES ('Reclamo');

INSERT INTO Role (rol_name)
VALUES ('Cargador');

INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
VALUES ('9784673', 'admin@vacanze.com', 'Virtuoso', 'Francisco', MD5('admin123'));

INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
VALUES ('10754927', 'checkin@vacanze.com', 'Gomes', 'Francisco', MD5('checkin123'));

INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
VALUES ('12345678', 'reclamo@vacanze.com', 'Omar', 'Jorge', MD5('reclamo123'));

INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
VALUES ('8654826', 'cargador@vacanze.com', 'Salazar', 'Marcos', MD5('cargador123'));

INSERT INTO Users(use_document_id, use_email, use_last_name, use_name, use_password)
VALUES ('20766589', 'cliente@vacanze.com', 'Martinez', 'Carlos', MD5('cliente123'));

INSERT INTO User_Role(usr_rol_id, usr_use_id) VALUES (2, 1);
INSERT INTO User_Role(usr_rol_id, usr_use_id) VALUES (3, 2);
INSERT INTO User_Role(usr_rol_id, usr_use_id) VALUES (4, 3);
INSERT INTO User_Role(usr_rol_id, usr_use_id) VALUES (5, 4);
INSERT INTO User_Role(usr_rol_id, usr_use_id) VALUES (1, 5);


------- grupo 6 ----------

INSERT INTO hotel (hot_nombre, hot_cant_habitaciones, hot_activo, hot_telefono, hot_sitio_web,
                   hot_fk_lugar)
VALUES ('Hotel 1', 50, TRUE, '1238123', null, 1);

-------------------------------Grupo 3---------------------------------------------------
INSERT INTO public.Plane(
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (nextval('seq_plane'),100,true, 50, 1000, 'Boeing 1');
INSERT INTO public.Plane(
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (nextval('seq_plane'),150,true, 40, 1000, 'Boeing 2');
INSERT INTO public.Plane(
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (nextval('seq_plane'),111,true, 30, 1000, 'Boeing 3');
INSERT INTO public.Plane(
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (nextval('seq_plane'),200,true, 50, 2000, 'Boeing 4');
INSERT INTO public.Plane(
	pla_id, pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (nextval('seq_plane'),80,true, 10, 1000, 'Boeing 5');
    
INSERT INTO public.Location(
	loc_city, loc_country)
	VALUES ('Venezuela', 'Caracas');
INSERT INTO public.Location(
	loc_city, loc_country)
	VALUES ('Germany', 'Berlin');
INSERT INTO public.Location(
	loc_city, loc_country)
	VALUES ('France', 'Paris');
INSERT INTO public.Location(
	loc_city, loc_country)
	VALUES ('Russian', 'Moscow');
INSERT INTO public.Location(
	loc_city, loc_country)
	VALUES ('Japan', 'Tokyo');
INSERT INTO public.Location(
	loc_city, loc_country)
	VALUES ('China', 'Beijing');