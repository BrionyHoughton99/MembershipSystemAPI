using MembershipSystem.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipSystem.Api.Services
{
    public class SessionService : ISessionService
    {
        #region Private Properties
        //using IHttpContextAccessor to allow use of httpContext in a service class not inheriting controllerbase - in controller class just use Http.Context.Session
        private IHttpContextAccessor httpContextAccessor;
        #endregion
        #region Constructors
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        #endregion

        public void StartSession(string cardNumber)
        {
            //this will set a session string for use when a user is logging in
            httpContextAccessor.HttpContext.Session.SetString("SessionID", cardNumber);

        }

        public void EndSession()
        {
            //this will remove the session string for use when a use is logging out 
            httpContextAccessor.HttpContext.Session.Remove("SessionID");
        }

        public bool IsSessionActive()
        {
            //checking if a session string has been set for use when checking if member is logged in 
            return (httpContextAccessor.HttpContext.Session.GetString("SessionID") != null);
        }
    }
}
