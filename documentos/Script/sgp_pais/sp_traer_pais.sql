drop function if exists sp_traer_pais;
CREATE OR REPLACE FUNCTION sp_traer_pais(
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
	
	RETURN QUERY 
	select
		p.num_sec
		,p.descripcion
		,p.estado
	from sgp_pais p
	where p.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_pais('1')
*/



