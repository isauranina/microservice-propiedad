drop function if exists sp_listado_propiedad;
CREATE OR REPLACE FUNCTION sp_listado_propiedad(
	valor_bus character varying, 
	parametro_bus character varying DEFAULT ''::character varying, 
	numeropaginaactual integer DEFAULT 0, 
	cantidadmostrar integer DEFAULT 0)
 RETURNS TABLE(
	num_sec bigint
	,descripcion character varying
	,direccion character varying
	,esverificado boolean
	,nsec_tipo_propiedad bigint
	,nsec_ciudad bigint
	,estado character varying 
	,total bigint
 )
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
begin
	--Si se trata de una busqueda con un parametro y valor variable
	-- SELECT public.sp_listado_propiedad('' ,'p.num_sec',0,10);
	sql = 'select 
		p.num_sec
		,p.descripcion
		,p.direccion
		,p.esverificado
		,p.nsec_tipo_propiedad
		,p.nsec_ciudad
		,p.estado
		,count(*) over () as total
		from sgp_propiedad p 
		where estado in (''AC'')';
	sql = sql || ' and upper(cast('|| parametro_bus ||' as varchar)) like ' || '''' || '%' || upper(valor_bus) || '%' || '''';
	
	sql = sql || ' order by num_sec ASC';
	
	if cantidadmostrar >  0 then
	sql = sql || ' limit ' || cantidadMostrar || ' offset ' || cantidadMostrar*numeroPaginaActual;
	end if;

	

	-- 	raise notice 'Value: %', sql;
	
	RETURN QUERY EXECUTE sql;
	
end
$function$
;

