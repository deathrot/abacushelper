using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortal.Session
{

    public class WebSessionTokenProvider : Logic.Interfaces.ISessionTokenProvider
    {
        private readonly Microsoft.AspNetCore.Http.IHeaderDictionary headerDictionary;

        public WebSessionTokenProvider(Microsoft.AspNetCore.Http.IHeaderDictionary headerDictionary)
        {
            this.headerDictionary = headerDictionary;
        }

        public string GetToken()
        {

            //this.headerDictionary.ContainsKey()
            return Guid.NewGuid().ToString() ;
            //string createPassPhrase = 
        }

        public bool IsSessionValid(Logic.ViewModels.SessionVM session, string token)
        {
            //session.AdditionalBlob = 
            throw new NotImplementedException();
        }
    }
}
