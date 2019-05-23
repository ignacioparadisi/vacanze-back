INSERT INTO lugar (l_tipo, l_nombre, fk_lugar)
VALUES ('P', 'Lugar de prueba', null);

INSERT INTO hotel (hot_nombre, hot_cant_habitaciones, hot_activo, hot_telefono, hot_sitio_web,
                   hot_fk_lugar)
VALUES ('Hotel 1', 50, TRUE, '1238123', null, 1);