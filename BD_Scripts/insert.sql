INSERT INTO lugar (l_tipo, l_nombre, fk_lugar)
VALUES ('P', 'Lugar de prueba', null),
       ('C', 'Ciudad de prueba', 1),
	   ('P', 'Lugar 2 de prueba', null),
	   ('C', 'Ciudad 2 de prueba', 2),
	   ('P', 'Lugar 3 de prueba', null),
	   ('C', 'Ciudad 3 de prueba', 3);

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

------- grupo 6 ----------

INSERT INTO hotel (hot_nombre, hot_cant_habitaciones, hot_activo, hot_telefono, hot_sitio_web,
                   hot_fk_lugar)
VALUES ('Hotel 1', 50, TRUE, '1238123', null, 1),
		('Hotel 2', 100, TRUE, '5456465', null, 1),
		('Hotel 3' , 200, TRUE, '54534', null , 2);