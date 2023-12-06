drop function if exists sp_traer_propiedad_estado;
CREATE OR REPLACE FUNCTION sp_traer_propiedad_estado(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nsec_propiedad bigint
	,nsec_estado bigint
	,estado character varying
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	--  select *from sp_traer_propiedad_estado('1')
	RETURN QUERY 
	select
		p.num_sec
		,p.nsec_propiedad
		,p.nsec_estado
		,p.estado
	from sgp_propiedad_estado p
	where p.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_propiedad_estado('1')
*/



