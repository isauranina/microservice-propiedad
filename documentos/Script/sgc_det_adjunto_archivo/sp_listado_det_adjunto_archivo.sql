drop function if exists sp_listado_det_adjunto_archivo;
CREATE OR REPLACE FUNCTION sp_listado_det_adjunto_archivo(
	valor_bus character varying, 
	parametro_bus character varying DEFAULT ''::character varying, 
	numeropaginaactual integer DEFAULT 0, 
	cantidadmostrar integer DEFAULT 0)
 RETURNS TABLE(
	num_sec bigint
	,nsec_adjunto bigint
	,nsec_nombre_tabla bigint
	,nsec_tabla bigint
	,nsec_usuario bigint
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
		d.num_sec
		,d.nsec_adjunto
		,d.nsec_nombre_tabla
		,d.nsec_tabla
		,d.nsec_usuario
		,d.estado
		,count(*) over () as total
		from sgc_det_adjunto_archivo d 
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

