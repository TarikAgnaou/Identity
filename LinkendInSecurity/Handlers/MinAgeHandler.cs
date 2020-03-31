using LinkendInSecurity.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkendInSecurity.Handlers
{
    public class MinAgeHandler : AuthorizationHandler<MinAgeRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirements requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Age"))
                return Task.CompletedTask;

            var age = int.Parse(context.User.Claims.First(c => c.Type == "Age").Value);
            if (age >= requirement.MinAge)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
