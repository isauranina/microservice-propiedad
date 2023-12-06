using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models.Data
{
    public class RespuestaDB
    {
        public string? status { get; set; }
        public string? response { get; set; }

        public string? numsec { get; set; }
    }
    
}
