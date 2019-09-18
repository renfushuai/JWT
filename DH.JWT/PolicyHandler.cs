using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DH.JWT
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            //赋值用户权限
            var userPermissions = requirement.UserPermissions;
            //从authorizationHandlerContext转成HttpContext 以便取出表请求信息
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;
            //请求URl
            var questUrl = httpContext.Request.Path.Value.ToUpperInvariant();
            //是否经过验证
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                if (userPermissions.GroupBy(g=>g.Url).Any(w=>w.Key.ToUpperInvariant()==questUrl))
                {
                    //用户名
                    var userName = httpContext.User.Claims.SingleOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;
                    if (userPermissions.Any(m=>m.UserName==userName&&m.Url.ToUpperInvariant()==questUrl))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        //无权限 跳转到拒绝页面
                        httpContext.Response.Redirect(requirement.DeniedAction);
                    }
                }
                else
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
