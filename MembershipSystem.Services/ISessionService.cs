using System;
using System.Collections.Generic;
using System.Text;

namespace MembershipSystem.Services
{
    public interface ISessionService
    {
        public void EndSession();
        public bool IsSessionActive();
        public void StartSession(string cardNumber);
    }
}
