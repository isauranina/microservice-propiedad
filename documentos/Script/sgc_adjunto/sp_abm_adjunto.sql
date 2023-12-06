drop function if exists sp_abm_adjunto;
CREATE OR REPLACE FUNCTION sp_abm_adjunto(
    accion int
	,_num_sec bigint
	,_nombre character varying default null
	,_nombre_fisico character varying default null
	,_tamano integer default null
	,_content_type character varying default null
	,_estado character varying default null
	,_nsec_usuario bigint default null
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
            
            --INSERTA DATOS
            insert into sgc_adjunto(
				nombre
				,nombre_fisico
				,tamano
				,content_type
				,estado
				,fecha_registro
				,fecha_modificacion
				,nsec_usuario
				) 
            values (
				trim(_nombre)
				,trim(_nombre_fisico)
				,_tamano
				,trim(_content_type)
				,'AC'
				,current_timestamp
				,current_timestamp
				,_nsec_usuario:: bigint
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
            UPDATE sgc_adjunto SET 
				nombre = trim(_nombre)
				,nombre_fisico = trim(_nombre_fisico)
				,tamano = _tamano
				,content_type = trim(_content_type)
				,fecha_modificacion = current_timestamp
				,nsec_usuario = _nsec_usuario:: bigint
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
            perform num_sec from sgc_adjunto where estado = 'BA' and num_sec = _num_sec::bigint limit 1;
            if found then
                return QUERY select 'error', 'El Registro ya ha sido Eliminado', '0';	
                return;
            end if;
            update sgc_adjunto
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
	--select * from sgc_adjunto

    v_date := 	(select current_timestamp);
	v_dato := 	('num_sec: ' || v_id::text || '|' ||
                (coalesce(_estado,'AC'))::text
                );
    select * into aux_respuesta from sp_abm_log(
			    1 --guardar
		    	,v_id
		   		,v_date
				,_nsec_usuario_registro
				,'sgc_adjunto'
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
