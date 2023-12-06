drop function if exists sp_traer_propiedad;
CREATE OR REPLACE FUNCTION sp_traer_propiedad(
	_num_sec bigint
)

RETURNS TABLE(
	id bigint
	,descripcion character varying
	,direccion character varying
	,esverificado boolean
	,tipo_propiedad  character varying
	,ciudad  character varying
	,precio int
)
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
BEGIN
	-- select * from sp_traer_propiedad(39)
	RETURN QUERY 
	select
		p.num_sec as id  
		,p.descripcion 
		,p.direccion 
		,p.esverificado
		,tp.nombre_tipo as tipo_propiedad 
		,c.descripcion as ciudad 
		,250 as precio
	from sgp_propiedad p
	inner join sgp_tipo_propiedad tp on p.nsec_tipo_propiedad =tp.num_sec 
	inner join sgp_ciudad c on p.nsec_ciudad =c.num_sec 
	where p.num_sec = _num_sec::bigint;
end;
$function$;
/*
select *from sp_traer_propiedad('4')
*/



