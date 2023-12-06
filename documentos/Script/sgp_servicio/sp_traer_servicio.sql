drop function if exists sp_traer_servicio;
CREATE OR REPLACE FUNCTION sp_traer_servicio(
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
	-- select *from sp_traer_servicio('1')
	RETURN QUERY 
	select
		s.num_sec
		,s.descripcion
		,s.estado
	from sgp_servicio s
	where s.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_servicio('1')
*/



