using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ISecurity
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
    }
}
