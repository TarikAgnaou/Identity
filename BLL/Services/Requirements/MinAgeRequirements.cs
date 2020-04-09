using Microsoft.AspNetCore.Authorization;

namespace BLL.Services.Requirements
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
