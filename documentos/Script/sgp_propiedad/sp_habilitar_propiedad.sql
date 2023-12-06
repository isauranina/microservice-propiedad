drop function if exists sp_cambiar_estado_propiedad;
CREATE OR REPLACE FUNCTION sp_cambiar_estado_propiedad(
		_num_sec bigint,
		_esverificado boolean default NULL
		)
RETURNS TABLE(
 	status text, 
 	response text, 
 	numsec text
)
 LANGUAGE plpgsql
AS $function$

declare
    filasAfectadas bigint;
    v_id bigint;
    v_date timestamp;     
begin

	-- SELECT sp_cambiar_estado_propiedad(4,true);
	-- sp_cambiar_estado_propiedad

	
		UPDATE sgp_propiedad SET 				
				esverificado = _esverificado				
		where num_sec = _num_sec::bigint;			
            v_id:= _num_sec;
            ------Valida si se afectaron filas----------------
            GET DIAGNOSTICS filasAfectadas = ROW_COUNT;	
            if filasAfectadas = 0 then
                return QUERY select 'error', 'No se pudo cambiar estado', '0';	
                return ;
            end if;
	
	 return QUERY select 'success', 'OK', v_id::text;
	
end
$function$
;

