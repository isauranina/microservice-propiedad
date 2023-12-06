drop function if exists sp_traer_tipo_propiedad;
CREATE OR REPLACE FUNCTION sp_traer_tipo_propiedad(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nombre_tipo character varying
	,estado character varying
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	
	RETURN QUERY 
	select
		t.num_sec
		,t.nombre_tipo
		,t.estado
	from sgp_tipo_propiedad t
	where t.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_tipo_propiedad('1')
*/



