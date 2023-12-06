drop function if exists sp_traer_adjunto;
CREATE OR REPLACE FUNCTION sp_traer_adjunto(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nombre character varying
	,nombre_fisico character varying
	,tamano integer
	,content_type character varying
	,estado character varying
	,nsec_usuario bigint
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	
	RETURN QUERY 
	select
		a.num_sec
		,a.nombre
		,a.nombre_fisico
		,a.tamano
		,a.content_type
		,a.estado
		,a.nsec_usuario
	from sgc_adjunto a
	where a.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_adjunto('1')
*/



