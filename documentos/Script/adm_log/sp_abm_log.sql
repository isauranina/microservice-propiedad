drop function if exists sp_abm_log;
CREATE OR REPLACE FUNCTION public.sp_abm_log(
accion integer
, _num_sec bigint DEFAULT NULL::bigint
, _fecha timestamp without time zone DEFAULT NULL::timestamp without time zone
, _nsec_usuario bigint DEFAULT NULL::bigint
, _nombre_tabla character varying DEFAULT NULL::character varying
, _tipo_accion integer DEFAULT NULL::integer
, _datos text DEFAULT NULL::text)
 RETURNS TABLE(status text, response text, numsec text)
 LANGUAGE plpgsql
AS $function$
declare
    filasAfectadas bigint;
    v_id bigint;
begin
    case accion	
        -- REGISTRAR
        when 1 then
        begin
            --INSERTA DATOS            	
	           -- select * from adm_log (1,'06/11/2023',1,'tabla_prueba',1,'datos'		 
			
           
            insert into adm_log(
				nsec_usuario
				,num_sec
				,fecha
				,tipo_accion
				,datos
				,nombre_tabla
				) 
            values (
				_nsec_usuario:: bigint
				,_num_sec:: bigint
				,_fecha:: timestamp without time zone
				,_tipo_accion
				,_datos
				,trim(_nombre_tabla)
				)
            RETURNING id INTO v_id;		
            ------Valida si se afectaron filas----------------
            GET DIAGNOSTICS filasAfectadas = ROW_COUNT;	
            if filasAfectadas = 0 then
                return QUERY select 'error', 'El Registro log no se pudo Guardar. Intente nuevamente', '0';
            else 
           		return QUERY select 'success', 'OK', _num_sec::text;
            end if;
           ---------------------------------------------------
        end;
        else 
            return QUERY select 'error', 'Ninguna Accion coincide', '0';
        
    end case;
END;
$function$
;
