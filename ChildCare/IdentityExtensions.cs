using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChildCare
{
    public static class IdentityExtensions
    {
        public static string GetProfilePhoto(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var photo = ci.FindFirstValue("ProfilePhoto");
                return ci.FindFirstValue("ProfilePhoto");
            }
            return null;
        }
    }
}
