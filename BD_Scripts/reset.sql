-- https://wiki.postgresql.org/wiki/Fixing_Sequences
-- psql -Atq -f reset.sql -o temp -d vacanza -U vacanza --password
-- psql -f temp -d vacanza -U vacanza --password
-- rm temp

-- Ejecutando los comandos de arriba actualizas los valores de todas las secuencias
-- de la base de datos, para evitar conflictos con los ID insertados manualmente en
-- inserts.sql

SELECT 'SELECT SETVAL(' ||
       quote_literal(quote_ident(PGT.schemaname) || '.' || quote_ident(S.relname)) ||
       ', COALESCE(MAX(' || quote_ident(C.attname) || '), 1) ) FROM ' ||
       quote_ident(PGT.schemaname) || '.' || quote_ident(T.relname) || ';'
FROM pg_class AS S,
     pg_depend AS D,
     pg_class AS T,
     pg_attribute AS C,
     pg_tables AS PGT
WHERE S.relkind = 'S'
  AND S.oid = D.objid
  AND D.refobjid = T.oid
  AND D.refobjid = C.attrelid
  AND D.refobjsubid = C.attnum
  AND T.relname = PGT.tablename
ORDER BY S.relname;