drop function if exists sp_abm_sgp_propiedad_estado;
CREATE OR REPLACE FUNCTION sp_abm_sgp_propiedad_estado(
    accion int
	,_num_sec bigint
	,_nsec_propiedad bigint default null
	,_nsec_estado bigint default null
	,_fecha_inicio timestamp without time zone default null
	,_fecha_fin timestamp without time zone default null
	,_estado character varying default null
    ,_nsec_usuario_registro bigint default null
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
   	v_dato text;
   	aux_respuesta RECORD;
begin
    case accion	
        -- REGISTRAR
        when 1 then
        begin
            --Valida Datos
            -- select * from sp_abm_sgp_propiedad_estado(1,2,4,1,'07-11-2023','08-11-2023','AC',1)
            --INSERTA DATOS
            insert into sgp_propiedad_estado(
				num_sec
				,nsec_propiedad
				,nsec_estado
				,fecha_inicio
				,fecha_fin
				,fecha_registro
				,estado
				) 
            values (
				_num_sec:: bigint
				,_nsec_propiedad:: bigint
				,_nsec_estado:: bigint
				,_fecha_inicio:: timestamp(1)
				,_fecha_fin:: timestamp(1)
				,current_timestamp		
				,'AC'
				)
            RETURNING num_sec INTO v_id;		
            ------Valida si se afectaron filas----------------
            GET DIAGNOSTICS filasAfectadas = ROW_COUNT;	
            if filasAfectadas = 0 then
                return QUERY select 'error', 'El Registro no se pudo Guardar', '0';
                return ;
            end if;
           ---------------------------------------------------
        end;

        --MODIFICAR
      	when 2 then
      	begin
          	--Valida Datos
            
            --ACTUALIZA DATOS
            UPDATE sgp_propiedad_estado SET 
				nsec_propiedad = _nsec_propiedad:: bigint
				,nsec_estado = _nsec_estado:: bigint
				,fecha_inicio = _fecha_inicio:: timestamp without time zone
				,fecha_fin = _fecha_fin:: timestamp without time zone
		where num_sec = _num_sec::bigint;			
            v_id:= _num_sec;
            ------Valida si se afectaron filas----------------
            GET DIAGNOSTICS filasAfectadas = ROW_COUNT;	
            if filasAfectadas = 0 then
                return QUERY select 'error', 'El Registro no se pudo Modificar', '0';	
                return ;
            end if;
           ---------------------------------------------------
        end;
    
        --CAMBIA DE ESTADO
        when 3 then
        begin			
            perform num_sec from sgp_propiedad_estado where estado = 'BA' and num_sec = _num_sec::bigint limit 1;
            if found then
                return QUERY select 'error', 'El Registro ya ha sido Eliminado', '0';	
                return;
            end if;
            update sgp_propiedad_estado
            set estado = 'BA' 
		where num_sec = _num_sec::bigint;			
            v_id:= _num_sec;
            ------Valida si se afectaron filas----------------
            GET DIAGNOSTICS filasAfectadas = ROW_COUNT;	
            if filasAfectadas = 0 then
           		return QUERY select 'error', 'El Registro no se pudo Eliminar', '0'; 
                return ;
            end if;
           ---------------------------------------------------
        end;
        else 
            return QUERY select 'error', 'Ninguna Accion coincide', '0';
        
    end case;

    -------adm_log---------
	--select * from sgp_propiedad_estado

    v_date := 	(select current_timestamp);
	v_dato := 	('num_sec: ' || v_id::text || '|' ||
				(coalesce(_nsec_propiedad,0))::text || '|' ||	
				(coalesce(_nsec_estado,0))::text || '|' ||	
				(coalesce(_fecha_inicio::text,''))::text || '|' ||	
				(coalesce(_fecha_fin::text,''))::text || '|' ||					
                (coalesce(_estado,'AC'))::text
                );
    select * into aux_respuesta from sp_abm_log(
			    1 --guardar
		    	,v_id
		   		,v_date
				,_nsec_usuario_registro
				,'sgp_propiedad_estado'
				,accion
				,v_dato
				);
	if aux_respuesta.status = 'error' then
   		return QUERY select aux_respuesta.status, aux_respuesta.response , aux_respuesta.numsec ;
   		return ;
    end if ;
   
    return QUERY select 'success', 'OK', v_id::text;

END;
$function$;
