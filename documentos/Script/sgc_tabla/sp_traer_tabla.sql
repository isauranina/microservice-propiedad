drop function if exists sp_traer_tabla;
CREATE OR REPLACE FUNCTION sp_traer_tabla(
	_num_sec integer
)

RETURNS TABLE(
	num_sec integer
	,nombre character varying
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
		,t.nombre
		,t.estado
	from sgc_tabla t
	where t.num_sec = _num_sec::integer;
end;
$function$;
/*
select *from sp_traer_tabla('1')
*/



