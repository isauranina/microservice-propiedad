drop function if exists sp_traer_estado_propiedad;
CREATE OR REPLACE FUNCTION sp_traer_estado_propiedad(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,descripcion character varying
	,estado character varying
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	-- select *from sp_traer_estado_propiedad('4')
	RETURN QUERY 
	select
		e.num_sec
		,e.descripcion
		,e.estado
	from sgp_estado_propiedad e
	where e.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_estado_propiedad('1')
*/



