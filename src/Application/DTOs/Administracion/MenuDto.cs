using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class MenuDto
    {
        public long id { get; set; }
        public string? title { get; set; }
        public string? routerlink { get; set; }
        public string? href { get; set; }
        public string? icon { get; set; }
        public string? target { get; set; }
        public bool hassubmenu { get; set; }
        public bool is_title { get; set; }
        public long parentid { get; set; }
        public string? parent { get; set; }

        public int orden { get; set; }
        public int total { get; set; }
    }
}
