using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Veneka.Platform.Common;

namespace ContactsManagement.API.Helpers
{
    public class SecurityContextHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static RequestContext GetCurrentRequestContext(ClaimsPrincipal user)
        {
            var subscriptionId = Guid.Empty.ToString();
            var emailAddress = string.Empty;
            var userId = Guid.Empty.ToString();

            RequestContext context =
               new RequestContext(Guid.NewGuid(), userId, Guid.Empty)
               {
                   UserEmail = emailAddress
               };
            return context;

        }
    }
}
