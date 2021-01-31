using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    public class TestLoginTokenProvider : Logic.Interfaces.ISessionTokenProvider
    {
        public string GetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public bool IsSessionValid(Logic.ViewModels.SessionVM session, string token)
        {
            return true;
        }
    }
}
