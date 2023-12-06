using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Rabbit
{
    public interface IAmqpService
    {
        public bool Publish(object objeto, string cola);
    }
}
