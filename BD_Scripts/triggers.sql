------------------------------Grupo 13-------------------------------------------
-- TRIGGER CON SU FUNCION que cambia el estado de un auto cuando se reserva
CREATE FUNCTION m13_automobilenotavaliable() returns TRIGGER
as $$
DECLARE
    fk_id INTEGER;
BEGIN
    fk_id := NEW.ra_id;
    UPDATE Automobile
    SET aut_isActive = false
    FROM Res_Aut ra
    WHERE aut_id = ra.ra_aut_fk and ra.ra_id = fk_id;

    RETURN NULL;
END
$$ Language plpgsql;


CREATE TRIGGER reservedAutomobile
    AFTER INSERT ON public.Res_Aut
    FOR EACH ROW
    EXECUTE PROCEDURE m13_automobilenotavaliable(ra_id)

-- TRIGGER CON SU FUNCION que cambia el estado de un auto cuando se cancela la  reserva
CREATE FUNCTION m13_automobileavaliable() returns TRIGGER
as $$
DECLARE
    fk_id INTEGER;
BEGIN
    fk_id := OLD.ra_id;
    UPDATE Automobile
    SET aut_isActive = true
    FROM Res_Aut ra
    WHERE aut_id = ra.ra_aut_fk and ra.ra_id = fk_id;

    RETURN NULL;
END
$$ Language plpgsql;


CREATE TRIGGER notReservedAutomobile
    BEFORE DELETE ON public.Res_Aut
    FOR EACH ROW
    EXECUTE PROCEDURE m13_automobileavaliable(ra_id)

------------------------------Fin grupo 13-------------------------------------------