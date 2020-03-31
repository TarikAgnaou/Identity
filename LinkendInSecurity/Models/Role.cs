using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkendInSecurity.Models
{
    public class Role : IdentityRole<Guid>
    {
        public static string[] Roles = new string[]
        {
            "Admin",
            "Visiteur",
            "Salarié",
            "Directeur"
        };
    }
}
