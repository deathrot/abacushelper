using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class LoginSessionProvider : Logic.Interfaces.ISessionTokenProvider
    {
        public string GetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public bool IsSessionValid(string token)
        {
            return true;
        }
    }
}
