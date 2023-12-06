drop function if exists sp_abm_propiedad;
CREATE OR REPLACE FUNCTION sp_abm_propiedad(
    accion int
	,_num_sec bigint
	,_descripcion character varying default null
	,_direccion character varying default null
	,_esverificado boolean default null
	,_nsec_tipo_propiedad bigint default null
	,_nsec_ciudad bigint default null
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
            
            --INSERTA DATOS
	        -- -- select * from sp_abm_propiedad(4,4,'descripcion','direccion' ,false,1,1,'AC',1)
	        
            insert into sgp_propiedad(
				descripcion
				,direccion
				,esverificado
				,nsec_tipo_propiedad
				,nsec_ciudad
				,estado
				) 
            values (
				trim(_descripcion)
				,trim(_direccion)
				,_esverificado
				,_nsec_tipo_propiedad:: bigint
				,_nsec_ciudad:: bigint
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
            UPDATE sgp_propiedad SET 
				descripcion = trim(_descripcion)
				,direccion = trim(_direccion)
				,esverificado = _esverificado
				,nsec_tipo_propiedad = _nsec_tipo_propiedad:: bigint
				,nsec_ciudad = _nsec_ciudad:: bigint
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
            perform num_sec from sgp_propiedad where estado = 'BA' and num_sec = _num_sec::bigint limit 1;
            if found then
                return QUERY select 'error', 'El Registro ya ha sido Eliminado', '0';	
                return;
            end if;
            update sgp_propiedad
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
       when 4 then
        begin	
	        --HABILITAR PROPIEDAD
            UPDATE sgp_propiedad SET 				
				esverificado = true 				
		where num_sec = _num_sec::bigint;			
            v_id:= _num_sec;
            ------Valida si se afectaron filas----------------
            GET DIAGNOSTICS filasAfectadas = ROW_COUNT;	
            if filasAfectadas = 0 then
                return QUERY select 'error', ' se pudo Habilitar', '0';	
                return ;
            end if;
           ---------------------------------------------------
	    end;
	   
        else 
            return QUERY select 'error', 'Ninguna Accion coincide', '0';
        
    end case;

    -------adm_log---------
	--select * from sgp_propiedad

    v_date := 	(select current_timestamp);
	v_dato := 	('num_sec: ' || v_id::text || '|' ||
				(coalesce(_descripcion,''))::text || '|' ||			
				(coalesce(_direccion,''))::text || '|' ||			
				--(coalesce(_esverificado,false))::text || '|' ||		
				(coalesce(_nsec_tipo_propiedad,0))::text || '|' ||		
				(coalesce(_nsec_ciudad,0))::text || '|' ||		
                (coalesce(_estado,'AC'))::text
                );
    select * into aux_respuesta from sp_abm_log(
			    1 --guardar
		    	,v_id
		   		,v_date
				,_nsec_usuario_registro
				,'sgp_propiedad'
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


