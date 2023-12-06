drop function if exists sp_listado_propiedad_estado;
CREATE OR REPLACE FUNCTION sp_listado_propiedad_estado(
	valor_bus character varying, 
	parametro_bus character varying DEFAULT ''::character varying, 
	numeropaginaactual integer DEFAULT 0, 
	cantidadmostrar integer DEFAULT 0)
 RETURNS TABLE(
	num_sec bigint
	,nsec_propiedad bigint
	,nsec_estado bigint
	,estado character varying 
	,total bigint
 )
 LANGUAGE plpgsql
AS $function$
declare
	sql VARCHAR;
begin
	--Si se trata de una busqueda con un parametro y valor variable
	-- SELECT public.sp_listado_propiedad_estado('' ,'p.num_sec',0,10);
	sql = 'select 
		p.num_sec
		,p.nsec_propiedad
		,p.nsec_estado
		,p.estado
		,count(*) over () as total
		from sgp_propiedad_estado p 
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

