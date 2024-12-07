using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public AuthorizeRole(params string[] roles)
        {
            this.allowedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["RolUsuario"] == null)
            {
                return false; 
            }
            var userRole = httpContext.Session["RolUsuario"].ToString();

            return Array.Exists(allowedRoles, role => role.Equals(userRole, StringComparison.InvariantCultureIgnoreCase));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "AccesoDenegado" }));
        }
    }
}