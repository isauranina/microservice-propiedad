create database db_propiedad

 
 
create table sgp_pais(
num_sec bigserial not null,
descripcion varchar(50) null,
estado varchar(2) null,
 primary key (num_sec)
);
create table sgp_ciudad(
num_sec bigserial not null,
nsec_pais bigint not null,
descripcion varchar(50) null,
estado varchar(2) null,
 primary key (num_sec)
);
ALTER TABLE sgp_ciudad ADD CONSTRAINT 
sgp_ciudad_fkey FOREIGN KEY (nsec_pais) REFERENCES sgp_pais(num_sec);

create table sgp_servicio(
num_sec bigserial not null,
descripcion varchar(50) null,
estado varchar(2) null,
 primary key (num_sec)
);

create table sgp_estado_propiedad(
num_sec bigserial not null,
descripcion varchar(50) null,
estado varchar(2) null,
 primary key (num_sec)
);
create table sgc_adjunto(
	num_sec bigserial NOT NULL,
	nombre varchar NULL,
	nombre_fisico varchar NULL,
	tamano int4 NULL,
	content_type varchar NULL,
	estado varchar(2) NOT NULL,
	fecha_registro timestamp(3) NOT NULL,
	fecha_modificacion timestamp(3) NULL,
	nsec_usuario int8 NULL,
	CONSTRAINT sgc_adjunto_pkey PRIMARY KEY (num_sec)
);
CREATE TABLE sgc_tabla (
	num_sec int4 NOT NULL,
	nombre varchar NOT NULL,
	estado varchar(2) NOT NULL,
	CONSTRAINT sga_tabla_pkey PRIMARY KEY (num_sec)
);
CREATE TABLE sgc_det_adjunto_archivo (
	num_sec bigserial NOT NULL,
	nsec_adjunto int8 NULL,
	nsec_nombre_tabla int8 NULL,
	nsec_tabla int8 NULL,	
	fecha_registro timestamp(3) NOT NULL,
	fecha_modificacion timestamp(3) NULL,
	nsec_usuario int8 NULL,
	estado varchar(2) NOT NULL,
	CONSTRAINT sgc_det_adjunto_archivo_pkey PRIMARY KEY (num_sec)
);
ALTER TABLE sgc_det_adjunto_archivo ADD CONSTRAINT sgc_det_adjunto_archivo_nsec_adjunto_fkey FOREIGN KEY (nsec_adjunto) REFERENCES sgc_adjunto(num_sec);
ALTER TABLE sgc_det_adjunto_archivo ADD CONSTRAINT sgc_det_adjunto_archivo_nsec_nombre_tabla_fkey FOREIGN KEY (nsec_nombre_tabla) REFERENCES sgc_tabla(num_sec);


create table sgp_tipo_propiedad(
num_sec bigserial not null,
nombre_tipo varchar(50) null,
estado varchar(2) null,
 primary key (num_sec)
);
create table sgp_propiedad(
num_sec bigserial not null,
descripcion varchar null,
direccion varchar(500) null,
esverificado boolean default false,
nsec_tipo_propiedad bigint not null,
nsec_ciudad bigint not null,
estado varchar(2) null,
 primary key (num_sec)
);
ALTER TABLE sgp_propiedad ADD CONSTRAINT nsec_tipo_propiedad_fkey FOREIGN KEY (nsec_tipo_propiedad) REFERENCES sgp_tipo_propiedad(num_sec);
ALTER TABLE sgp_propiedad ADD CONSTRAINT nsec_ciudad_fkey FOREIGN KEY (nsec_ciudad) REFERENCES sgp_ciudad(num_sec);

create table sgp_propiedad_estado(
num_sec bigserial not null,
nsec_propiedad bigint,
nsec_estado bigint,
fecha_inicio timestamp(3) without time zone null,
fecha_fin timestamp(3) without time zone null,
fecha_registro timestamp(3) without time zone null,
estado varchar(2) null,
 primary key (num_sec)
);
ALTER TABLE sgp_propiedad_estado ADD CONSTRAINT nsec_propiedad_fkey FOREIGN KEY (nsec_propiedad) REFERENCES sgp_propiedad(num_sec);
ALTER TABLE sgp_propiedad_estado ADD CONSTRAINT nsec_estado_fkey FOREIGN KEY (nsec_estado) REFERENCES sgp_estado_propiedad(num_sec);

create table sgp_propiedad_servicio(
num_sec bigserial not null,
nsec_propiedad bigint,
nsec_servicio bigint,
descripcion varchar(500),
fecha_registro timestamp(3) without time zone null,
estado varchar(2) null,
 primary key (num_sec)
);
ALTER TABLE sgp_propiedad_servicio ADD CONSTRAINT nsec_propiedad_fkey 
FOREIGN KEY (nsec_propiedad) REFERENCES sgp_propiedad(num_sec);
ALTER TABLE sgp_propiedad_servicio ADD CONSTRAINT nsec_servicio_fkey FOREIGN KEY (nsec_servicio) REFERENCES sgp_servicio(num_sec);

create table sgp_reglas_propiedad(
num_sec bigserial not null,
nsec_propiedad bigint,
descripcion varchar(500),
fecha_registro timestamp(3) without time zone null,
estado varchar(2) null,
 primary key (num_sec)
);
ALTER TABLE sgp_reglas_propiedad ADD CONSTRAINT nsec_propiedad_fkey 
FOREIGN KEY (nsec_propiedad) REFERENCES sgp_propiedad(num_sec);


CREATE TABLE public.adm_log (
	id bigserial NOT NULL,
	num_sec int8 NOT NULL,
	fecha timestamp NULL,
	nsec_usuario int8 NULL,
	nombre_tabla varchar(150) NULL,
	tipo_accion int4 NULL,
	datos text NULL,
	CONSTRAINT adm_log_pkey PRIMARY KEY (id)
);

CREATE OR REPLACE FUNCTION public.sp_abm_log(accion integer, _num_sec bigint DEFAULT NULL::bigint, _fecha timestamp without time zone DEFAULT NULL::timestamp without time zone, _nsec_usuario bigint DEFAULT NULL::bigint, _nombre_tabla character varying DEFAULT NULL::character varying, _tipo_accion integer DEFAULT NULL::integer, _datos text DEFAULT NULL::text)
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
            --Valida Datos
            	--select * from adm_log
            --INSERTA DATOS
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








