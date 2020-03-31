using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkendInSecurity.Requirements
{
    public class MinAgeRequirements : IAuthorizationRequirement
    {
        public int MinAge { get; set; }

        public MinAgeRequirements(int minAge)
        {
            MinAge = minAge;
        }
    }
}
