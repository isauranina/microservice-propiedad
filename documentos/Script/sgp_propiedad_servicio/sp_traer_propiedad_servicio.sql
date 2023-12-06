drop function if exists sp_traer_propiedad_servicio;
CREATE OR REPLACE FUNCTION sp_traer_propiedad_servicio(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nsec_propiedad bigint
	,nsec_servicio bigint
	,descripcion character varying
	,estado character varying
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
begin
	-- select *from sp_traer_propiedad_servicio('4')
	
	RETURN QUERY 
	select
		p.num_sec
		,p.nsec_propiedad
		,p.nsec_servicio
		,p.descripcion
		,p.estado
	from sgp_propiedad_servicio p
	where p.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_propiedad_servicio('1')
*/



