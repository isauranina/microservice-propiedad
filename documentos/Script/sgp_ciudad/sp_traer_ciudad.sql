drop function if exists sp_traer_ciudad;
CREATE OR REPLACE FUNCTION sp_traer_ciudad(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nsec_pais bigint
	,descripcion character varying
	,estado character varying
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	
	RETURN QUERY 
	select
		c.num_sec
		,c.nsec_pais
		,c.descripcion
		,c.estado
	from sgp_ciudad c
	where c.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_ciudad('1')
*/



