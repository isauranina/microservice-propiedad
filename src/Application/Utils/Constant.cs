using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
   
    public static class Estados
    {
        public const string Activo = "AC";
        public const string Aprobado = "AP";
        public const string Baja = "BA";
        public const string Pendiente = "PE";
        public const string Consolidado = "CO";
        public const string Rechazado = "RE";

    }

    public static class Status
    {
        public const string Empty = "empty";
        public const string Success = "success";
        public const string Error = "error";
        public const string Error500 = "500";

    }

    public static class Roles
    {
        public const string Todos = "Administrador,Revisor,Entidad,Mae";
        public const string Administrador = "Administrador";
        public const string Entidad = "Entidad";
        public const string Revisor = "Revisor";
        public const string Mae = "Mae";


    }
    public static class ClaimCustom
    {
        public const string NsecUsuario = "sid";
        public const string NsecRol = "nsecRol";
    }

    public static class JasperReportObject
    {
        public const int VALOR_BLANCO_PDF = 987;
    }
}
