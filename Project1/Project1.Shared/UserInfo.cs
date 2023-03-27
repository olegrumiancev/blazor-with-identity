using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Shared
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public List<string> ExposedClaims { get; set; }
    }
}
