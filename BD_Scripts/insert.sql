INSERT INTO "Hotel" (hot_id, hot_nombre, hot_hora_entrada, hot_hora_salida, hot_telefono, hot_sitio_web) VALUES
(1, 'Blue Sea Beach Hotel', '16:00:00', '11:00:00', '+1 858 488-4700', 'https://www.blueseabeachhotel.com/');

INSERT INTO "Servicio" (ser_id, ser_nombre) VALUES 
(1, 'WiFi en Lobby gratis');

INSERT INTO "Hotel_Servicio" (hot_ser_fk_hot_id, hot_ser_fk_ser_id, hot_ser_destacado) VALUES 
(1, 1, TRUE);

COMMIT; 