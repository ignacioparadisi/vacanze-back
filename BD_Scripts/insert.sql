INSERT INTO lugar (l_tipo, l_nombre, fk_lugar)
VALUES ('P', 'Lugar de prueba', null);

INSERT INTO hotel (hot_nombre, hot_cant_habitaciones, hot_activo, hot_telefono, hot_sitio_web,
                   hot_fk_lugar)
VALUES ('Hotel 1', 50, TRUE, '1238123', null, 1);

-------------------------------Grupo 3---------------------------------------------------
INSERT INTO public.Plane(
	pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (100,true, 50, 1000, 'Boeing 1');
INSERT INTO public.Plane(
	pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (150,true, 40, 1000, 'Boeing 2');
INSERT INTO public.Plane(
	pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (111,true, 30, 1000, 'Boeing 3');
INSERT INTO public.Plane(
	pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (200,true, 50, 2000, 'Boeing 4');
INSERT INTO public.Plane(
	pla_autonomy, "pla_isActive", pla_capacity, "pla_loadingCap", pla_model)
	VALUES (80,true, 10, 1000, 'Boeing 5');
    
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