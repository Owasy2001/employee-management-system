using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAASShopDataAccess;
using SAASShopDomain;

namespace Hemaiya.Middleware
{
    public class PrivilegedResourceRequirement : IAuthorizationRequirement
    {
        public int PrivilegedResource { get; set; }

        public PrivilegedResourceRequirement(int resource)
        {
            PrivilegedResource = resource;
        }
    }

    public class PrivilegedResourceHandler : AuthorizationHandler<PrivilegedResourceRequirement>
    {
        private readonly ISecurity SecurityMan;

        public PrivilegedResourceHandler(ISecurity security)
        {
            SecurityMan = security;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PrivilegedResourceRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            string role = context.User.Claims.FirstOrDefault(s => s.Type == System.Security.Claims.ClaimTypes.Role).Value;

            int userId = int.Parse(context.User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);
            bool hasPrivilige = SecurityMan.CheckUserPrivilege(requirement.PrivilegedResource, userId);

            if (!hasPrivilige)
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class AuthorizePrivilegePolicyAttribute : TypeFilterAttribute
    {
        public AuthorizePrivilegePolicyAttribute(PrivilegedResource Policy) : base(typeof(AuthorizePrivilegePolicyFilter))
        {
            Arguments = new object[] { Policy };
        }
    }

    public class AuthorizePrivilegePolicyFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authService;
        public PrivilegedResource _policy { get; set; }

        public AuthorizePrivilegePolicyFilter(PrivilegedResource policy, IAuthorizationService authorization)
        {
            _policy = policy;
            authService = authorization;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string description = _policy.ToString();

            var authorized = await authService.AuthorizeAsync(context.HttpContext.User, description);

            if (authorized.Succeeded)
            {
                return;
            }

            context.Result = new ForbidResult();
            return;
        }
    }

    public class RestrictUserByCasePolicyAttribute : TypeFilterAttribute
    {
        public RestrictUserByCasePolicyAttribute(string policy) : base(typeof(RestrictUserByCasePolicyFilter))
        {
            Arguments = new object[] { policy };
        }
    }

    public class RestrictUserByCasePolicyFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authService;
        public string _policy { get; set; }

        public RestrictUserByCasePolicyFilter(string policy, IAuthorizationService authorization)
        {
            _policy = policy;
            authService = authorization;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string description = _policy.ToString();

            var authorized = await authService.AuthorizeAsync(context.HttpContext.User, description);

            var caseVal = string.Empty;

            if (context.HttpContext.Request.RouteValues["caseId"] != null)
            {
                caseVal = context.HttpContext.Request.RouteValues["caseId"].ToString();
            }
            else if (context.HttpContext.Request.Query["caseId"].ToString() != null)
            {
                caseVal = context.HttpContext.Request.Query["caseId"].ToString();
            }

            if (authorized.Succeeded && (!context.HttpContext.User.HasClaim(c => c.Type == caseVal) || string.IsNullOrEmpty(caseVal)))
            {
                return;
            }

            context.Result = new ForbidResult();
            return;
        }
    }
}