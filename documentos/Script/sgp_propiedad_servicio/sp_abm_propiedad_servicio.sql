drop function if exists sp_abm_propiedad_servicio;
CREATE OR REPLACE FUNCTION sp_abm_propiedad_servicio(
    accion int
	,_num_sec bigint
	,_nsec_propiedad bigint default null
	,_nsec_servicio bigint default null
	,_descripcion character varying default null
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
            -- select * from sp_abm_propiedad_servicio(1,1,4,1,'descripcion','AC',1)
            --INSERTA DATOS
            insert into sgp_propiedad_servicio(
				nsec_propiedad
				,nsec_servicio
				,descripcion
				,fecha_registro
				,estado
				) 
            values (
				_nsec_propiedad:: bigint
				,_nsec_servicio:: bigint
				,trim(_descripcion)
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
            UPDATE sgp_propiedad_servicio SET 
				nsec_propiedad = _nsec_propiedad:: bigint
				,nsec_servicio = _nsec_servicio:: bigint
				,descripcion = trim(_descripcion)
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
            perform num_sec from sgp_propiedad_servicio where estado = 'BA' and num_sec = _num_sec::bigint limit 1;
            if found then
                return QUERY select 'error', 'El Registro ya ha sido Eliminado', '0';	
                return;
            end if;
            update sgp_propiedad_servicio
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
	--select * from sgp_propiedad_servicio

    v_date := 	(select current_timestamp);
	v_dato := 	('num_sec: ' || v_id::text || '|' ||
				(coalesce(_nsec_propiedad,0))::text || '|' ||	
				(coalesce(_nsec_servicio,0))::text || '|' ||	
				(coalesce(_descripcion,''))::text || '|' ||	
                (coalesce(_estado,'AC'))::text
                );
    select * into aux_respuesta from sp_abm_log(
			    1 --guardar
		    	,v_id
		   		,v_date
				,_nsec_usuario_registro
				,'sgp_propiedad_servicio'
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
