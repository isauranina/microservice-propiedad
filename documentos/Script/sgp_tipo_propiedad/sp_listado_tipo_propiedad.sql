drop function if exists sp_listado_tipo_propiedad;
CREATE OR REPLACE FUNCTION sp_listado_tipo_propiedad(
	valor_bus character varying, 
	parametro_bus character varying DEFAULT ''::character varying, 
	numeropaginaactual integer DEFAULT 0, 
	cantidadmostrar integer DEFAULT 0)
 RETURNS TABLE(
	num_sec bigint
	,nombre_tipo character varying
	,estado character varying 
	,total bigint
 )
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
begin
	--Si se trata de una busqueda con un parametro y valor variable
	
	sql = 'select 
		t.num_sec
		,t.nombre_tipo
		,t.estado
		,count(*) over () as total
		from sgp_tipo_propiedad t 
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

