drop function if exists sp_listado_adjunto;
CREATE OR REPLACE FUNCTION sp_listado_adjunto(
	valor_bus character varying, 
	parametro_bus character varying DEFAULT ''::character varying, 
	numeropaginaactual integer DEFAULT 0, 
	cantidadmostrar integer DEFAULT 0)
 RETURNS TABLE(
	num_sec bigint
	,nombre character varying
	,nombre_fisico character varying
	,tamano integer
	,content_type character varying
	,estado character varying
	,nsec_usuario bigint 
	,total bigint
 )
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
begin
	--Si se trata de una busqueda con un parametro y valor variable
	
	sql = 'select 
		a.num_sec
		,a.nombre
		,a.nombre_fisico
		,a.tamano
		,a.content_type
		,a.estado
		,a.nsec_usuario
		,count(*) over () as total
		from sgc_adjunto a 
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

