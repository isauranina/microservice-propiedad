drop function if exists sp_traer_reglas_propiedad;
CREATE OR REPLACE FUNCTION sp_traer_reglas_propiedad(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nsec_propiedad bigint
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
		r.num_sec
		,r.nsec_propiedad
		,r.descripcion
		,r.estado
	from sgp_reglas_propiedad r
	where r.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_reglas_propiedad('1')
*/



