drop function if exists sp_traer_det_adjunto_archivo;
CREATE OR REPLACE FUNCTION sp_traer_det_adjunto_archivo(
	_num_sec bigint
)

RETURNS TABLE(
	num_sec bigint
	,nsec_adjunto bigint
	,nsec_nombre_tabla bigint
	,nsec_tabla bigint
	,nsec_usuario bigint
	,estado character varying
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	
	RETURN QUERY 
	select
		d.num_sec
		,d.nsec_adjunto
		,d.nsec_nombre_tabla
		,d.nsec_tabla
		,d.nsec_usuario
		,d.estado
	from sgc_det_adjunto_archivo d
	where d.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_det_adjunto_archivo('1')
*/



